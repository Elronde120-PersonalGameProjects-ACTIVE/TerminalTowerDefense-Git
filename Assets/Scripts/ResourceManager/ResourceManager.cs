using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : GameplayComponent
{
    public static ResourceManager instance;
    public Resources startingResources;
    private Resources playerResources;
    private bool isReady = false;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        playerResources = new Resources(startingResources);
        isReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Gets a refernce to the players resources
    /// </summary>
    /// <returns>Returns a (PBV) reference to the players resources if IsReady == true, false otherwise</returns>
    public Resources GetPlayerResources(){
        if(IsReady())
            return playerResources;

        return null;
    }

    public bool SetResources(Resources newResources){
        if(IsReady()){
            playerResources.CopyResources(newResources);
        }
        return false;
    }

    public override bool IsReady(){
        return isReady;
    }

}
