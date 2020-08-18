using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for showing a preview of node locations before game startup
/// </summary>
[ExecuteInEditMode]
public class NodeManagerPreview : MonoBehaviour
{
    public int numRows = 0, numColumns = 0;
    public float sphearSize = 1f;
    public bool drawLines = true;

    public Vector2 spacing;
    public Vector2Int playerBaseStart;
    public Vector2Int enemyStart;
    public Vector2Int[] enemyPath;


   // Start is called before the first frame update
    /// <summary>
    /// Callback to draw gizmos only if the object is selected.
    /// </summary>
    void OnDrawGizmosSelected()
    {
        //draw sphears
        for(int i = 0; i < numRows; i++){
            for(int j = 0; j < numColumns; j++){
                Vector2 pos = new Vector2(i,j);

                if(pos == playerBaseStart){
                    Gizmos.color = Node.PLAYER_BASE_COLOR;
                }
                else if(pos == enemyStart){
                    Gizmos.color = Node.ENEMY_BASE_COLOR;
                }else{
                    Gizmos.color = Node.PLAYER_TOWER_COLOR;
                    
                    if(enemyPath != null){
                        for(int z = 0; z < enemyPath.Length; z++){
                            if(pos == enemyPath[z]){
                                Gizmos.color = Node.ENEMY_PATH_COLOR;
                            }
                        }
                    }
                }
                 Gizmos.DrawSphere(new Vector3(i * (1 + spacing.x),j * (1 + spacing.y),0),sphearSize);
            }
        }

        //draw enemy path
        if(drawLines){
            Gizmos.color = Node.ENEMY_PATH_COLOR;
            for(int i = 0; i < enemyPath.Length; i++){
                if(i == 0){
                    Gizmos.DrawLine((Vector3Int)enemyStart, (Vector3Int)enemyPath[i]);
                    if(i < enemyPath.Length - 1){
                        Gizmos.DrawLine((Vector3Int)enemyPath[i], (Vector3Int)enemyPath[i + 1]);
                    }
                }else if(i < enemyPath.Length - 1){
                    Gizmos.DrawLine((Vector3Int)enemyPath[i], (Vector3Int)enemyPath[i + 1]);
                }else{
                    Gizmos.DrawLine((Vector3Int)enemyPath[i], (Vector3Int)playerBaseStart);
                }
            }
        }
       
    }
}
