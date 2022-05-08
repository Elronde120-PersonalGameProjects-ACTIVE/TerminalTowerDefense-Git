using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConsoleTowerDefense.AI.Spawner
{
    public class AISpawner : MonoBehaviour
    {
        public static AISpawner instance;
        public AIWaveData currentWave => levelWaves[currentWaveIndex];
        public int currentWaveTickSpawnRate => levelWaves[currentWaveIndex].ticksBetweenSpawns;

        public int ticksBetweenWaves = 50;
        public List<AIWaveData> levelWaves;

        public AISpawnProvider spawnProvider { get { return GetSpawnProvider(); } set { SetSpawnProvider(value); } }
        private AISpawnProvider _spawnProvider;
        private int internalTick = 0;

        int currentWaveIndex = 0;

        //checking bools
        public bool spawningWave { get; set; }
        public bool reachedTickBetweenWaves => internalTick % ticksBetweenWaves == 0 && internalTick != 0;

        public bool canSpawnWave => reachedTickBetweenWaves && Utils.IsValidIndex(currentWaveIndex, (ICollection)levelWaves) && spawningWave == false;
        
        public bool Started { get; private set; }

        private void Start()
        {
            instance = this;
            spawningWave = false;
            
        }


        private void Update()
        {
            if (canSpawnWave)
            {
                //start next wave
                StartCoroutine(SpawnWave(levelWaves[currentWaveIndex]));

            }
        }

        IEnumerator SpawnWave(AIWaveData wave)
        {
            spawningWave = true;

            int newinternalTick = 0;
            for (int i = 0; i < wave.count; i++)
            {
                Spawn(wave.enemyToSpawn.prefab);
                newinternalTick = internalTick + wave.ticksBetweenSpawns;
                yield return new WaitUntil(() => internalTick == newinternalTick);
            }
        

            currentWaveIndex++;
            spawningWave = false;
        }

        void Spawn(GameObject objectToSpawn)
        {
            if (spawnProvider == null)
                return;

            spawnProvider.Spawn(objectToSpawn);
        }

        AISpawnProvider GetSpawnProvider()
        {
            if (_spawnProvider == null)
                _spawnProvider = GetComponent<AISpawnProvider>();

            return _spawnProvider;
        }

        void SetSpawnProvider(AISpawnProvider newSpawnProvider)
        {
            _spawnProvider = newSpawnProvider;
        }

        public void StartWaveSpawning()
        {
            if (Started)
            {
                return;
            }

            Started = true;
            if (spawnProvider == null)
            {
                Debug.LogError("ERROR: " + this.name + " does not have a AISpawnProvider attached to it. Cannot spawn enemies!");
            }
            else
            {
                TimeTickSystem.onTick += (object sender, TimeTickSystem.OnTickEventArgs args) => internalTick++; //subscription will be relagated to a command (I.E. start)
            }
        }
    }
}
