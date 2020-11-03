using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower Database Entry", menuName = "ScriptableObjects/Tower/Tower Database Entry", order = 1)]
public class TowerDBEntry : ScriptableObject
{
    
    public string towerName;
    public string towerDescription;
    public GameObject towerGameobject;
    public TowerStats stats;
    
    
    public override string ToString(){
        return "name: " + towerName + "\n" + towerDescription + "\n" + stats;
    }

}
