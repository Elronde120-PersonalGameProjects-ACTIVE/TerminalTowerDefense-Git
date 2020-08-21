using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceCommand : Command
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
        
        if(args.Length >= 3 && GameManager.instance.ReadyForPlay()){
            yield return StartCoroutine(Terminal.sCommandDatabase["remove"].Excecute(args[1], args[2]));      
            yield return StartCoroutine(Terminal.sCommandDatabase["place"].Excecute(args[0], args[1], args[2]));
        }else{
            Terminal.PrintToTerminal("Incorrect usage: " + GetUsage());
             yield return false;
        }

        yield return true;
    }

    public override ManPage Man(){
        if(internalManPage == null){
            internalManPage = new ManPage();
            internalManPage.commandName = "replace";
            internalManPage.usage = "replace [TowerName] [posX] [posY]";
            internalManPage.description = "Calls remove and place commands with given arguments";
            internalManPage.example = "replace Shooter 0 2";
        }

        return internalManPage;
    }
}
