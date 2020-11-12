using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AI Wave Data", menuName = "ScriptableObjects/AI/AI Wave Data", order = 0)]
public class AIWaveData : ScriptableObject {
    public int ticksBetweenSpawns;
    public List<GameObject> aiToSpawn;
}