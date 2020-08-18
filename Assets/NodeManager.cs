using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns and provides access to all nodes in the level
/// </summary>
public class NodeManager : MonoBehaviour
{
    public int numRows = 0, numColumns = 0;
    public float rowOffset, columnOffset;

    /// <summary>
    /// Has this script finished its startup?
    /// </summary>
    /// <value></value>
    public bool isReady {get; private set;}

    public NodeManager instance;
    private Node[,] nodes;
    public Node nodePrefab;
    public GameObject nodeParent;
    private Vector2Int playerBaseStart;
    private Vector2Int enemyStart;
    private Vector2Int[] enemyPath;
    public NodeManagerPreview preview;
    // Start is called before the first frame update
    void Start()
    {
        isReady = false;
        instance = this;       
        CopyPreview(preview);
        CreateNodes();
        isReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateNodes(){
        nodes = new Node[numRows, numColumns];
        for(int i = 0; i < numRows; i++){
            for(int j = 0; j < numColumns; j++){
                nodes[i,j] = Instantiate(nodePrefab.gameObject, new Vector3(i * (1 + columnOffset),j * (1 + rowOffset),0), Quaternion.identity, nodeParent.transform).GetComponent<Node>();
                Vector2 pos = new Vector2(i,j);

                if(pos == playerBaseStart){
                    nodes[i,j].nodeType = Node.NodeType.PLAYER_BASE;
                }
                else if(pos == enemyStart){
                    nodes[i,j].nodeType = Node.NodeType.ENEMY_BASE;
                }else{
                    nodes[i,j].nodeType = Node.NodeType.PLAYER_TOWER;
                    
                    if(enemyPath != null){
                        for(int z = 0; z < enemyPath.Length; z++){
                            if(pos == enemyPath[z]){
                                nodes[i,j].nodeType = Node.NodeType.ENEMY_PATH;
                            }
                        }
                    }
                }
                nodes[i,j].ColorNode(); //DEBUG LINE
            }
        }
    }

    void CopyPreview(NodeManagerPreview preview){
        if(preview != null){
            numColumns = preview.numColumns;
            numRows = preview.numColumns;
            columnOffset = preview.spacing.x;
            rowOffset = preview.spacing.y;
            playerBaseStart = preview.playerBaseStart;
            enemyStart = preview.enemyStart;
            enemyPath = preview.enemyPath;
        }
    }
}
