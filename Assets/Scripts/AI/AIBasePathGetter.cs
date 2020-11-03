using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AIBasePathGetter : MonoBehaviour, IAIPathProvider
{
    public Vector2Int[] GetPath(){
        Vector2Int[] path = NodeManager.instance.enemyPath;
        Array.Resize<Vector2Int>(ref path, path.Length + 1);
        path[path.Length - 1] = NodeManager.instance.playerBaseStart;
        return path;
    }
}
