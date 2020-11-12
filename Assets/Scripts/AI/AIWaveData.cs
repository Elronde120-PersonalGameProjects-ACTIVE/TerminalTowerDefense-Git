﻿using System.Collections.Generic;
using UnityEngine;

namespace ConsoleTowerDefense.AI.Spawner
{
    [CreateAssetMenu(fileName = "New AI Wave Data", menuName = "ScriptableObjects/AI/AI Wave Data", order = 0)]
    public class AIWaveData : ScriptableObject
    {


        public AIData enemyToSpawn;
        public int count;
        public int ticksBetweenSpawns;
        
    }
}


