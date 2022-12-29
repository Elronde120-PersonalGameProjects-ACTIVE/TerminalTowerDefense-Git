using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private AIBaseController target;
    
    private TowerStats internalStats;

    private int m_currentFireTick = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        sprite.sprite = initialSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    void OnTick(object sender, TimeTickSystem.OnTickEventArgs e)
    {
        UpdateSprite(sender, e);
        UpdateTarget(sender, e);
        UpdateFireLogic(sender, e);
    }

    void UpdateSprite(object sender, TimeTickSystem.OnTickEventArgs e){
        if(target != null){
            //angle calculations on sprites assume gameobject will NOT be rotating
            Vector3 targetDir = target.transform.position - gameObject.transform.position;
            Vector3 forward = transform.up;
            float angle = Vector3.SignedAngle(forward, targetDir, Vector3.forward);

            Debug.DrawLine(transform.position, target.transform.position, Color.green);
            
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

    void UpdateTarget(object sender, TimeTickSystem.OnTickEventArgs e)
    {

        if (stats == null)
        {
            return;
        }

        if (stats.targetableTags == null)
        {
            return;
        }

        if (target == null)
        {
            SelectTarget();
        }
    }

    void UpdateFireLogic(object sender, TimeTickSystem.OnTickEventArgs e)
    {
        if(stats == null)
        {
            return;
        }

        // Increase current fire tick
        m_currentFireTick++;

        if (target == null || m_currentFireTick < internalStats.tickFireRate)
        {
            return;
        }

        // We can shoot, reset current fire tick and shoot at the target
        m_currentFireTick = 0;
        Shoot();
    }

    void Shoot()
    {
        if(target == null)
        {
            return;
        }

        target.TakeDamage(internalStats.damage);
    }

    void SelectTarget()
    {
        // Create a list of all enemies in the scene
        var enemies = new List<AIBaseController>();

        // Search for all enemies
        foreach (var tag in stats.targetableTags)
        {
            // Get all enemies with the current tag and add them to the enemies list
            var enemyGameObjects = GameObject.FindGameObjectsWithTag(tag).ToList<GameObject>();
            enemyGameObjects.ForEach((enemy) => enemies.Add(enemy.GetComponent<AIBaseController>()));
        }

        // If no enemies, return early
        if (enemies.Count == 0)
        {
            return;
        }

        // Find the closest enemy that is within bounds
        float shortestDistance = Mathf.Infinity;
        AIBaseController closestEnemy = null;
        foreach (var enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && distanceToEnemy <= stats.range)
            {
                shortestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        // Set to our target that is selected (if any)
        target = closestEnemy;
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnEnable()
    {
        TimeTickSystem.onTick += OnTick;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        TimeTickSystem.onTick -= OnTick;
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
        
        /*if (target != null)
        {
            Gizmos.DrawLine(transform.position, target.position);
        }*/
    }
}
