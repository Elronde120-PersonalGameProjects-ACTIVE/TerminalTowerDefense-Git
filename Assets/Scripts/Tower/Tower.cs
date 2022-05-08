using System;
using System.Collections;
using System.Collections.Generic;
using ConsoleTowerDefense.AI;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Sprite initialSprite;
    public SpriteRenderer sprite;
    public TowerStats stats {get {return internalStats;} set {internalStats = value;}   }
    [SerializeField]
    private SpriteAnglePair[] leftSprites;

    [SerializeField]
    private SpriteAnglePair[] rightSprites;
    private Transform target;
    private AIBaseController targetAIComponent;
    
    private TowerStats internalStats;

    private int currentFireTick = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        sprite.sprite = initialSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    void UpdateSprite(object sender, TimeTickSystem.OnTickEventArgs e){
        if(target != null){
            //angle calculations on sprites assume gameobject will NOT be rotating
            Vector3 targetDir = target.transform.position - gameObject.transform.position;
            Vector3 forward = transform.up;
            float angle = Vector3.SignedAngle(forward, targetDir, Vector3.forward);

            Debug.DrawLine(transform.position, target.transform.position, Color.green, 1f);
            
            for(int i = 0; i < leftSprites.Length; i++){
                if(angle <= leftSprites[i].maxAngle && angle >= leftSprites[i].minAngle){
                    sprite.sprite = leftSprites[i].sprite;
                    break;
                }else if(angle >= rightSprites[i].maxAngle && angle <= rightSprites[i].minAngle){
                    sprite.sprite = rightSprites[i].sprite;
                    break;
                }          
            }
        }
    }

    void UpdateTarget(object sender, TimeTickSystem.OnTickEventArgs e){
        if(stats != null){
            if(stats.targetableTags != null && target == null){
                List<GameObject> enemies = new List<GameObject>();
                for(int i = 0; i < stats.targetableTags.Length; i++){
                    enemies.AddRange(GameObject.FindGameObjectsWithTag(stats.targetableTags[i]));
                }

                float shortestDistance = Mathf.Infinity;
                GameObject nearestEnemy = null;
                foreach(GameObject enemy in enemies){
                    float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                    if(distanceToEnemy < shortestDistance){
                        shortestDistance = distanceToEnemy;
                        nearestEnemy = enemy;
                    }
                }

                if(nearestEnemy != null && shortestDistance <= stats.range){
                    target = nearestEnemy.transform;
                    nearestEnemy.TryGetComponent<AIBaseController>(out targetAIComponent);
                }else{
                    target = null;
                }
            }
        }
    }

    void UpdateFireLogic(object sender, TimeTickSystem.OnTickEventArgs e)
    {
        if (stats != null)
        {
            currentFireTick++;

            if (currentFireTick >= internalStats.tickFireRate)
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        if (targetAIComponent != null)
        {
            targetAIComponent.TakeDamage(internalStats.damage);
        }
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnEnable()
    {
        TimeTickSystem.onTick += UpdateTarget;
        TimeTickSystem.onTick += UpdateSprite;
        TimeTickSystem.onTick += UpdateFireLogic;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        TimeTickSystem.onTick -= UpdateTarget;
        TimeTickSystem.onTick -= UpdateSprite;
    }

    [System.Serializable]
    class SpriteAnglePair{
        public Sprite sprite;
        public float maxAngle;
        public float minAngle;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, internalStats.range);
        
        if (target != null)
        {
            Gizmos.DrawLine(transform.position, target.position);
        }
    }
}
