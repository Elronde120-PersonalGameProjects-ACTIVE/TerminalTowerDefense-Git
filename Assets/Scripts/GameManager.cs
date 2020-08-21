using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private bool isReady = false;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        StartCoroutine(ReadyForPlayCor());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// Are all the necessary scripts ready for the game to start?
    /// </summary>
    /// <returns>True if all the scripts are ready, false otherwise</returns>
    public bool ReadyForPlay(){
        return isReady;
    }

    IEnumerator ReadyForPlayCor(){
        while(Terminal.instance == null || Terminal.instance.isReady == false){
            if(Terminal.instance == null){
                Debug.LogError("GameManager: no instance of Terminal detected!");
            }
            yield return null;
        }

        while(TowerDatabase.instance == null || TowerDatabase.instance.IsReady() == false){
            if(TowerDatabase.instance == null){
                Debug.LogError("GameManager: no instance of TowerDatabase detected!");
            }
            yield return null;
        }

        
        while(ResourceManager.instance == null || ResourceManager.instance.IsReady() == false){
            if(ResourceManager.instance == null){
                Debug.LogError("GameManager: no instance of ResourceManager detected!");
            }
            yield return null;
        }

        while(NodeManager.instance == null || NodeManager.instance.IsReady() == false){
            if(NodeManager.instance == null){
                Debug.LogError("GameManager: no instance of NodeManager detected!");
            }
            yield return null;
        }

        //all scripts ready, start main loop
        StartCoroutine(Terminal.instance.MainLoop());
        isReady = true;
    }
}
