using System.Collections;
using System.Collections.Generic;
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
    
    private TowerStats internalStats;
    
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
            if(stats.targetableTags != null){
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
                }else{
                    target = null;
                }
            }
        }
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnEnable()
    {
        TimeTickSystem.onTick += UpdateTarget;
        TimeTickSystem.onTick += UpdateSprite;
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
}
