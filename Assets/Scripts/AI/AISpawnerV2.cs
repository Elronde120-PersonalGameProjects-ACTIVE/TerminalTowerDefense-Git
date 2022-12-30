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
        private int m_currentWaveIndex = 0;

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

            StartCoroutine(SpawnWavesCoroutine());
        }

        private IEnumerator SpawnWavesCoroutine()
        {
            // If we have no waves to spawn, or we are not spawning waves, break out
            if(m_waveData.Count == 0 || SpawningWaves == false)
            {
                yield break;
            }

            Terminal.PrintToTerminal($"Spawning wave {m_currentWaveIndex + 1}/{m_waveData.Count}");

            // Wait for the wave spawn delay
            var waveSpawnDelay = m_internalTickCounter + m_waveStartDelayTicks;
            yield return new WaitUntil(() => m_internalTickCounter == waveSpawnDelay);

            // Get the current wave
            var currentWave = m_waveData[m_currentWaveIndex];

            // Spawn each group in the wave
            foreach(var group in currentWave.Groups)
            {
                // Wait for the group start spawn delay
                var waitTime = m_internalTickCounter + group.GroupStartDelayTicks;
                yield return new WaitUntil(() => m_internalTickCounter == waitTime);

                // Start spawning in the AI group
                for (int i = 0; i < group.count; i++)
                {
                    // Place the enemy at the spawn point
                    var spawnedAIData = group.enemyToSpawn;
                    Instantiate(spawnedAIData.prefab, new Vector3(NodeManager.instance.enemyStart.x, NodeManager.instance.enemyStart.y), Quaternion.identity);

                    // Wait for the enemy to move before spawning another enemy
                    waitTime = m_internalTickCounter + spawnedAIData.navigationTickInterval;
                    yield return new WaitUntil(() => m_internalTickCounter == waitTime);
                }

            }
            
            OnWaveFinishedSpawning();
            Terminal.PrintToTerminal("Wave Finished");

            if (m_currentWaveIndex>= m_waveData.Count)
            {
                // We have spawned all waves, return here
                OnAllWavesSpawned();
                yield break;
            }         
        }

        private void OnAllWavesSpawned()
        {
            
        }

        private void OnWaveFinishedSpawning()
        {
            SpawningWaves = false;

            // Increase wave index
            m_currentWaveIndex += 1;
        }
    }
}

