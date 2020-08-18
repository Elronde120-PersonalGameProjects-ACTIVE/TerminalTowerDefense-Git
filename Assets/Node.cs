using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Node : MonoBehaviour
{
    public NodeType nodeType = NodeType.NEUTRAL;
    private SpriteRenderer spriteRenderer;

    public static Color ENEMY_BASE_COLOR = new Color(255,188,0);
    public static Color ENEMY_PATH_COLOR = new Color(255,0,0);
    public static Color PLAYER_BASE_COLOR = new Color(0,46,229);
    public static Color PLAYER_TOWER_COLOR = new Color(0,0,255);
    public static Color NEUTRAL_COLOR = new Color(255,255,255);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ColorNode(){
        if(nodeType == NodeType.ENEMY_BASE){
            GetSpriteRenderer().color = ENEMY_BASE_COLOR;
        }
        else if(nodeType == NodeType.ENEMY_PATH){
            GetSpriteRenderer().color = ENEMY_PATH_COLOR;
        }
        else if(nodeType == NodeType.PLAYER_TOWER){
            GetSpriteRenderer().color = PLAYER_TOWER_COLOR;
        }
        else if(nodeType == NodeType.PLAYER_BASE){
            GetSpriteRenderer().color = PLAYER_BASE_COLOR;
        }else{
             GetSpriteRenderer().color = NEUTRAL_COLOR;
        }
    }

    public bool HasChild(){
        return transform.childCount != 0;
    }

    SpriteRenderer GetSpriteRenderer(){
        if(spriteRenderer == null){
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        return spriteRenderer;
    }

    public enum NodeType{
        PLAYER_TOWER,
        PLAYER_BASE,
        ENEMY_BASE,
        ENEMY_PATH,
        NEUTRAL
    }
}
