using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ConsoleTowerDefense.AI.Spawner
{
    
    public class AIBaseSpawner : MonoBehaviour, AISpawnProvider
    {
        public void Spawn(UnityEngine.GameObject AIToSpawn)
        {
            Debug.Log("Spawning " + AIToSpawn);
            Instantiate(AIToSpawn, new Vector3(NodeManager.instance.enemyStart.x, NodeManager.instance.enemyStart.y), Quaternion.identity);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
