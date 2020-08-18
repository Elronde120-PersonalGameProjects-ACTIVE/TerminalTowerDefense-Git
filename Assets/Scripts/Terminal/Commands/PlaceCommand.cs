﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceCommand : Command
{

    //TODO:
    //Reference tower database here
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

    public override void Excecute(params string[] args){
        
        if(NodeManager.instance != null && NodeManager.instance.IsReady()){
            if(args.Length >= 3){
                int posX;
                int posY;

                if(int.TryParse(args[1], out posX) == false){
                    Terminal.PrintToTerminal("posX must be a number!");
                    return;
                }

                if(int.TryParse(args[2], out posY) == false){
                    Terminal.PrintToTerminal("posY must be a number!");
                    return;
                }

                Node placementNode = NodeManager.instance.GetNode(posX, posY);
                if(placementNode == null){
                    Terminal.PrintToTerminal("either posX or posY is not a valid location!");
                    return;
                }

                if(placementNode.nodeType == Node.NodeType.PLAYER_TOWER){
                    if(placementNode.HasChild()){
                        Terminal.PrintToTerminal(posX + " " + posY + " already has a tower!");
                        return;
                    }
                    if(TowerDatabase.instance.Check(args[0]) == false){
                        Terminal.PrintToTerminal(args[0] + " is not a valid name!");
                        return;
                    }

                    //subtract resources here
                    Instantiate(TowerDatabase.instance.Get(args[0]).towerGameobject, placementNode.gameObject.transform.position, Quaternion.identity, placementNode.transform);
                    //apply stats to tower script on towerGameobject here
                }
                else{
                    Terminal.PrintToTerminal(posX + " " + posY + " is not a valid tower location!");
                    return;
                }

                Terminal.PrintToTerminal( args[0] + " tower placed successfully at " + posX + " " + posY + "!");

            }else{
                Terminal.PrintToTerminal("Incorrect usage: " + GetUsage());
            }
        }
        else{
            Terminal.PrintToTerminal("NodeManager is either not setup or not ready for input!");
        }
    }

    public override ManPage Man(){
        if(internalManPage == null){
            internalManPage = new ManPage();
            internalManPage.commandName = "place";
            internalManPage.usage = "place [TowerName] [posX] [posY]";
            internalManPage.description = "Places a tower with the given TowerName at posX posY if it is a valid position";
            internalManPage.example = "place Shooter 0 2";
        }

        return internalManPage;
    }
}
