using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConsoleTowerDefense.AI.Spawner
{
    public class AISpawnerV2 : MonoBehaviour
    {
        public static AISpawnerV2 Instance;

        // FLAGS
        /// <summary>
        /// Are we currently spawning waves?
        /// </summary>
        public bool SpawningWaves { get; private set; } = false;

        /// <summary>
        /// A list filled with all wave data for this level
        /// </summary>
        [SerializeField] private List<AIWaveData> m_waveData = new List<AIWaveData>();

        /// <summary>
        /// The current wave index to be used in <see cref="m_waveData"/>. set to -1 if waves have not started
        /// </summary>
        private int m_currentWaveIndex = -1;

        private int m_internalTickCounter = 0;

        [SerializeField] private int m_waveStartDelayTicks = 0;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            Instance = this;
        }

        private void Start()
        {
            TimeTickSystem.onTick += OnTick;
        }

        private void OnTick(object sender, TimeTickSystem.OnTickEventArgs e)
        {
            m_internalTickCounter++;
        }

        // Start spawning waves
        public void StartWaveSpawning()
        {
            // Return out if we already are spawning waves
            if(SpawningWaves)
            {
                return;
            }

            SpawningWaves = true;

            // Reset current wave index
            m_currentWaveIndex = 0;

            StartCoroutine(SpawnWavesCoroutine());
        }

        private IEnumerator SpawnWavesCoroutine()
        {
            // If we have no waves to spawn, or we are not spawning waves, break out
            if(m_waveData.Count == 0 || SpawningWaves == false)
            {
                yield break;
            }

            // Wait for the wave spawn delay
            var waveSpawnDelay = m_internalTickCounter + m_waveStartDelayTicks;
            yield return new WaitUntil(() => m_internalTickCounter == waveSpawnDelay);

            // Get the current wave
            var currentWave = m_waveData[m_currentWaveIndex];

            // Spawn each enemy in the wave
            for(int i = 0; i < currentWave.count; i++)
            {
                // Place the enemy at the spawn point
                var spawnedAIData = currentWave.enemyToSpawn;
                Instantiate(spawnedAIData.prefab, new Vector3(NodeManager.instance.enemyStart.x, NodeManager.instance.enemyStart.y), Quaternion.identity);

                // Wait for the enemy to move before spawning another enemy
                var waitTime = m_internalTickCounter + spawnedAIData.navigationTickInterval;
                yield return new WaitUntil(() => m_internalTickCounter == waitTime);
            }

            // Increase wave index
            m_currentWaveIndex += 1;

            if(m_currentWaveIndex>= m_waveData.Count)
            {
                // We have spawned all waves, return here
                OnAllWavesSpawned();
                yield break;
            }

            // Start the next wave
            StartCoroutine(SpawnWavesCoroutine());
        }

        private void OnAllWavesSpawned()
        {
            SpawningWaves = false;
        }
    }
}

