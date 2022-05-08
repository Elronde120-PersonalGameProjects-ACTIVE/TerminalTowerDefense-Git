using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTickSystem : MonoBehaviour
{
    public class OnTickEventArgs : EventArgs {
        public int tick;
    }
    public static event EventHandler<OnTickEventArgs> onTick;
    public static float TICK_TIMER_MAX = 0.2f; //5 ticks per second
    private int tick;
    private float tickTimer;

    public static bool Paused = false;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Update()
    {
        if (Paused)
        {
            return;
        }
        
        tickTimer += Time.deltaTime;
        if(tickTimer >= TICK_TIMER_MAX){
            tickTimer -= TICK_TIMER_MAX;
            tick++;
            onTick?.Invoke(this, new OnTickEventArgs {tick = tick});
        }
    }
}
