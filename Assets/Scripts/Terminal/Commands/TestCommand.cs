using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCommand : Command
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

    public override IEnumerator Excecute(params string[] args){
        Terminal.PrintToTerminal("Test Command: Printing to terminal!");
        Terminal.PrintToTerminal("Test Command: args:");
        for(int i = 0; i < args.Length; i++){
            Terminal.PrintToTerminal("Test Command: args[" + i + "]: " + args[i]);
        }
        yield return true;
    }

    public override ManPage Man(){
        if(internalManPage == null){
            internalManPage = new ManPage();
            internalManPage.commandName = "test";
            internalManPage.usage = "test";
            internalManPage.description = "A test command, does nothing";
            internalManPage.example = "test";
        }

        return internalManPage;
    }
}
