﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower DB Entry", menuName = "ScriptableObjects/TowerDBEntry", order = 1)]
public class TowerDBEntry : ScriptableObject
{
    
    public string towerName;
    public string towerDescription;
    public GameObject towerGameobject;
    public TowerStats stats;
    
    

}
