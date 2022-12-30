using System.Collections;
using System.Collections.Generic;
using ConsoleTowerDefense.AI.Spawner;
using UnityEditorInternal;
using UnityEngine;

public class StartCommand : Command
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
        if (AISpawnerV2.Instance.SpawningWaves)
        {
            yield break;
        }
        AISpawnerV2.Instance.StartWaveSpawning();
        yield return null;
    }

    public override ManPage Man()
    {
        return new ManPage()
        {
            commandName = "start",
            description = "Starts the simulation. Using this command more than once does nothing",
            example = "start",
            usage = "start"
        };
    }
}
