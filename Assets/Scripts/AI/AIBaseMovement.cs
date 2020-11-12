using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConsoleTowerDefense.AI
{
    public class AIBaseMovement : MonoBehaviour, IAIMovementProvider
    {
        public void Move(Vector2Int[] waypoints, ref int currentTargetWaypoint, float speed)
        {
            if (currentTargetWaypoint < waypoints.Length)
            {
                transform.Translate((new Vector3(waypoints[currentTargetWaypoint].x, waypoints[currentTargetWaypoint].y) - transform.position).normalized * speed, Space.World);

                if (currentTargetWaypoint < waypoints.Length && Vector2.Distance(waypoints[currentTargetWaypoint], transform.position) < 0.1f)
                {
                    currentTargetWaypoint++;
                }
            }
        }
    }
}