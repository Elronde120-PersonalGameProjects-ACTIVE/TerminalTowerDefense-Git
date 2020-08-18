using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for holding reference to all spawnable tower gameobjects through a dictionary
/// </summary>
public class TowerDatabase : GameplayComponent
{
    public TowerDatabase instance;
     private bool isReady = false;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        isReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Has this script finished its startup?
    /// </summary>
    /// <value></value>
    public override bool IsReady(){
        return isReady;
    }
}
