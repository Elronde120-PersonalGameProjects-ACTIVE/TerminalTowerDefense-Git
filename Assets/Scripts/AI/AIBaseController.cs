using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBaseController : MonoBehaviour
{
    public AIData baseData;
    private IAIMovementProvider movement;
    private IAIPathProvider pathGetter;
    private Vector2Int[] path;
    private int currentPathTarget;
    private int currentNavigationTick = 0;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<IAIMovementProvider>();
        pathGetter = GetComponent<IAIPathProvider>();

        if(movement == null){
            Debug.LogError("ERROR: " + this.gameObject.name + " does not have an IAIMovment interface script on it. It cannot preform any movement!");
        }else{
            TimeTickSystem.onTick += OnTick;
        }

        if(pathGetter == null){
            Debug.LogError("ERROR: " + this.gameObject.name + " does not have an IAIPathGetter interface script on it. It cannot preform any movement!");
        }else{
            path = pathGetter.GetPath();
        }
    }

    void OnTick(object sender, TimeTickSystem.OnTickEventArgs args){
        if(movement == null || pathGetter == null)
            return;
        currentNavigationTick += 1;

        if(currentNavigationTick > baseData.navigationTickRate){
            movement.Move(path, ref currentPathTarget, baseData.moveSpeed);             
            currentNavigationTick = 0;
        }
    }
}
