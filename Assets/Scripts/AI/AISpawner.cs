using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour
{
    public List<GameObject> currentWave => levelWaves[currentWaveIndex].aiToSpawn;
    public int currentWaveTickSpawnRate => levelWaves[currentWaveIndex].ticksBetweenSpawns;
    public int ticksUntilStart = 50;
    public int ticksBetweenWaves = 50;
    public List<AIWaveData> levelWaves;
    public AISpawnProvider spawnProvider {get {return GetSpawnProvider();} set {SetSpawnProvider(value);}}
    private AISpawnProvider _spawnProvider;
    private int internalTick = 0;

    int currentWaveIndex = 0;

    //checking bools
    public bool spawningWave {get; set;}
    public bool reachedTickBetweenWaves => internalTick % ticksBetweenWaves == 0;
    public bool IsValidIndex(int index, ICollection collection) => index < collection.Count && index >= 0;
    public bool canSpawnWave => reachedTickBetweenWaves && IsValidIndex(currentWaveIndex,(ICollection)levelWaves) && spawningWave == false;

    private void Start() {
        if(spawnProvider == null){
            Debug.LogError("ERROR: " + this.name + " does not have a AISpawnProvider attached to it. Cannot spawn enemies!");
        }else{
            TimeTickSystem.onTick += (object sender, TimeTickSystem.OnTickEventArgs args) => internalTick++; //subscription will be relagated to a command (I.E. start)
        }
    }


    private void Update() {
        if(canSpawnWave){
            //start next wave
            StartCoroutine(SpawnWave(levelWaves[currentWaveIndex]));
            currentWaveIndex++; 
        }
    }

    IEnumerator SpawnWave(AIWaveData wave){
        spawningWave = true;
        foreach(GameObject obj in currentWave){
            //wait until internalTick is a multiple of ticksBetweenSpawns
            yield return new WaitUntil(() => internalTick % levelWaves[currentWaveIndex].ticksBetweenSpawns == 0); 
            spawnProvider.Spawn(obj);
        }
        spawningWave = false;
    }

    void Spawn(GameObject objectToSpawn){
        if(spawnProvider == null)
            return;

        spawnProvider.Spawn(objectToSpawn);
    }

    AISpawnProvider GetSpawnProvider(){
        return _spawnProvider;
    }
    
    void SetSpawnProvider(AISpawnProvider newSpawnProvider){
        _spawnProvider = newSpawnProvider;
    }
}
