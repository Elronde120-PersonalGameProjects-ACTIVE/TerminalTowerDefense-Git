using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowersCommand : Command
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
        foreach(string name in TowerDatabase.instance.GetTowerNames()){
            Terminal.PrintToTerminal(name);
        }
        yield return true;
    }

    public override ManPage Man(){
        if(internalManPage == null){
            internalManPage = new ManPage();
            internalManPage.commandName = "towers";
            internalManPage.usage = "towers";
            internalManPage.description = "Prints the names of all available towers";
            internalManPage.example = "towers";
        }

        return internalManPage;
    }
}
