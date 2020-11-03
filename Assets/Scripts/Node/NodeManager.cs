using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns and provides access to all nodes in the level
/// </summary>
public class NodeManager : GameplayComponent
{
    public int numRows = 0, numColumns = 0;
    public float rowOffset, columnOffset;
    private bool isReady = false;
    public static NodeManager instance;
    private Node[,] nodes;
    public Node nodePrefab;
    public GameObject nodeParent;
    public Vector2Int playerBaseStart {get; private set;}
    private Vector2Int enemyStart;
    public Vector2Int[] enemyPath {get; private set;}
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

    /// <summary>
    /// Gets a node object at indexes posX posY
    /// </summary>
    /// <param name="posX">column index</param>
    /// <param name="posY">row index</param>
    /// <returns>A valid node object if posX and posY are valid indexes, null otherwise</returns>
    public Node GetNode(int posX, int posY){
        if(IsValidIndex(posX).Value && IsValidIndex(posY).Key){
            return nodes[posX, posY];
        }

        return null;
    }

    /// <summary>
    /// Checks if the given index is valid for indexing nodes, key is for row indexing, column is for column indexing
    /// </summary>
    /// <param name="index">The index to check</param>
    /// <returns>
    /// Returns:
    /// <true, false> if the index is only valid for row indexing
    /// <false, true> if the index is only valid for column indexing
    /// <false, false> if the index is not valid at all
    /// <true, true> if the index is only valid for both row and column indexing
    /// </returns>
    public KeyValuePair<bool, bool> IsValidIndex(int index){
        bool isValidRow = false, isValidColumn = false;
        if(index >= 0  && index < numRows){
            isValidRow = true;
        }

        if(index >= 0  && index < numColumns){
            isValidColumn = true;
        }

        return new KeyValuePair<bool, bool>(isValidRow, isValidColumn);
    }

    /// <summary>
    /// Has this script finished its startup?
    /// </summary>
    /// <value></value>
    public override bool IsReady(){
        return isReady;
    }
}
