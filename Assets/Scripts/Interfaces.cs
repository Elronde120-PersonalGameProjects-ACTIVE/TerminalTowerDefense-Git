using UnityEngine;

public interface IAIMovementProvider 
{
    void Move(Vector2Int[] waypoints, ref int currentTargetWaypoint, float speed);
}

public interface IAIPathProvider{
    Vector2Int[] GetPath();
}

public interface AISpawnProvider{
    void Spawn(GameObject AIToSpawn);

}

