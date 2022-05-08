using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCommand : Command
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override string GetCommandName(){
        return Man().commandName;
    }

    public override string GetUsage(){
        return Man().usage;
    }

    public override IEnumerator Excecute(params string[] args)
    {
        TimeTickSystem.Paused = !TimeTickSystem.Paused;
        Terminal.PrintToTerminal($"Simulation is now {(TimeTickSystem.Paused ? "paused" :"unpaused")}");
        yield return null;
    }

    public override ManPage Man()
    {
        return new ManPage()
        {
            commandName = "pause",
            description = "Pauses/Unpauses the games simulation speed",
            example = "pause"
        };
    }
}
