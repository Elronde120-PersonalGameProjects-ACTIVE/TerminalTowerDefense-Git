using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveCommand : Command
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
         if(NodeManager.instance != null && NodeManager.instance.IsReady()){
            if(args.Length >= 2){
                int posX;
                int posY;

                if(int.TryParse(args[0], out posX) == false){
                    Terminal.PrintToTerminal("posX must be a number!");
                    yield return false;
                }

                if(int.TryParse(args[1], out posY) == false){
                    Terminal.PrintToTerminal("posY must be a number!");
                    yield return false;
                }

                Node placementNode = NodeManager.instance.GetNode(posX, posY);
                if(placementNode == null){
                    Terminal.PrintToTerminal("either posX or posY is not a valid location!");
                    yield return false;
                }
                
                if(placementNode.HasChild()){
                    Destroy(placementNode.transform.GetChild(0).gameObject);
                    //return % of spent resources here
                    Terminal.PrintToTerminal("Tower removed successfully at " + posX + " " + posY + "!");
                }
            }
            else{
                Terminal.PrintToTerminal("Incorrect usage: " + GetUsage());
                yield return false;
            }
        }
        else{
            Terminal.PrintToTerminal("NodeManager is either not setup or not ready for input!");
            yield return false;
        }
        yield return true;
    }

    public override ManPage Man(){
        if(internalManPage == null){
            internalManPage = new ManPage();
            internalManPage.commandName = "remove";
            internalManPage.usage = "place [posX] [posY]";
            internalManPage.description = "Removes a tower at posX posY and returns a portion of the resources spent to place that tower";
            internalManPage.example = "remove 0 2";
        }

        return internalManPage;
    }
}
