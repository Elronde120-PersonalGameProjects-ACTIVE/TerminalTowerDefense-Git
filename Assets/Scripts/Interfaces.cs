using UnityEngine;

namespace ConsoleTowerDefense.AI
{
    public interface IAIMovementProvider
    {
        void Move(Vector2Int[] waypoints, ref int currentTargetWaypoint, float speed);
    }

    public interface IAIPathProvider
    {
        Vector2Int[] GetPath();
    }
}

namespace ConsoleTowerDefense.AI.Spawner
{
    public interface AISpawnProvider
    {
        void Spawn(GameObject AIToSpawn);

    }
}

