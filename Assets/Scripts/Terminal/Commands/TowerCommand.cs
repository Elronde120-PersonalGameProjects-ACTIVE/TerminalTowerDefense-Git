using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCommand : Command
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
       if(args.Length >= 1 && GameManager.instance.ReadyForPlay()){
            if(TowerDatabase.instance.Check(args[0]) == false){
                Terminal.PrintToTerminal("Unknown Tower!");
                yield break;
            }

            TowerDBEntry tower = TowerDatabase.instance.Get(args[0]);
            Terminal.PrintToTerminal(tower.ToString());

        }else{
            Terminal.PrintToTerminal("Incorrect usage: " + GetUsage());
             yield return true;
        }

        yield return true;
    }

    public override ManPage Man(){
        if(internalManPage == null){
            internalManPage = new ManPage();
            internalManPage.commandName = "tower";
            internalManPage.usage = "tower [TowerName]";
            internalManPage.description = "Prints the details of a specific tower";
            internalManPage.example = "tower Gunner";
        }

        return internalManPage;
    }
}
