using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandsCommand : Command
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
        
        foreach(KeyValuePair<string, Command> command in Terminal.sCommandDatabase){
            Terminal.PrintToTerminal(command.Value.GetCommandName());
        }

        yield return true;
    }

    public override ManPage Man(){
        if(internalManPage == null){
            internalManPage = new ManPage();
            internalManPage.commandName = "commands";
            internalManPage.usage = "commands";
            internalManPage.description = "Prints all command names, usage, and descriptions.";
            internalManPage.example = "commands";
        }

        return internalManPage;
    }
}
