using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManCommand : Command
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
        if(args.Length >= 1){
            if(Terminal.sCommandDatabase.ContainsKey(args[0]) == false){
                Terminal.PrintToTerminal("Unknown command to print man of!");
                yield break;
            }

            Terminal.PrintToTerminal(Terminal.sCommandDatabase[args[0]].Man().usage);
            Terminal.PrintToTerminal(Terminal.sCommandDatabase[args[0]].Man().description);
            Terminal.PrintToTerminal(Terminal.sCommandDatabase[args[0]].Man().example);

        }else{
            Terminal.PrintToTerminal("Incorrect usage: " + GetUsage());
             yield break;
        }

        yield return true;
    }

    public override ManPage Man(){
        if(internalManPage == null){
            internalManPage = new ManPage();
            internalManPage.commandName = "man";
            internalManPage.usage = "man [CommandName]";
            internalManPage.description = "Prints the man page for a specific command";
            internalManPage.example = "man place";
        }

        return internalManPage;
    }
}
