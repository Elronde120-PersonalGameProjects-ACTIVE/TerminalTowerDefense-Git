using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower Stats", menuName = "ScriptableObjects/TowerStats", order = 1)]
public class TowerStats : ScriptableObject
{
    public Resources cost;
    public int damage;
    public float range;

    public string[] targetableTags;
    
    public override string ToString(){
        string initial = "cost:\n" + cost + "damage: " + damage + "\nrange: " + range;
        initial += "\nTargetable:";
        if(targetableTags != null && targetableTags.Length > 0){
            for(int i = 0; i < targetableTags.Length; i++){
                initial += "\n" + targetableTags[i];
            }
        }else{
            initial += "\nNone";
        }
        return initial;
    }

}
