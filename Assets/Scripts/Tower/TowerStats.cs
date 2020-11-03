using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower Stats", menuName = "ScriptableObjects/Tower/Tower Stats", order = 1)]
public class TowerStats : ScriptableObject
{
    public Resources cost;

    /// <summary>
    /// How much damage this tower will deal to its target
    /// </summary>
    public int damage;

    /// <summary>
    /// The maximum range this tower can target and shoot
    /// </summary>
    public float range;

    /// <summary>
    /// How many ticks until this tower can fire
    /// </summary>
    public float tickFireRate;

    /// <summary>
    /// What this tower can target
    /// </summary>
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
