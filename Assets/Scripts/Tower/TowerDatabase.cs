using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for holding reference to all spawnable tower gameobjects through a dictionary
/// </summary>
public class TowerDatabase : GameplayComponent
{
    public static TowerDatabase instance;
    private bool isReady = false;
    private Dictionary<string, TowerDBEntry> towerDB = new Dictionary<string, TowerDBEntry>();
    private List<string> towerNames = new List<string>();

    [SerializeField]
    private TowerDBEntry[] initialTowerDatabase;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        for(int i = 0; i < initialTowerDatabase.Length; i++){
                    
            if(Check(initialTowerDatabase[i].name)){
                Debug.LogError("TowerDatabase: Detected a duplicate key, check names in initialTowerDatabase! Continuing..");
                continue;
            }
            Add(initialTowerDatabase[i]);
            towerNames.Add(initialTowerDatabase[i].towerName);
        }
        isReady = true;
    }

    /// <summary>
    /// Gets a tower gameobject from the database
    /// </summary>
    /// <param name="name">The name of the tower to get</param>
    /// <returns>Returns a valid gameobject of name corosponds to a entry in the DB, null otherwise</returns>
    public TowerDBEntry Get(string name){
        if(IsReady()){
            if(Check(name) == false){
                return null;
            }else{
                return towerDB[name];
            }
        }

        return null;

    }

    /// <summary>
    /// Creates a new entry in the DB based on given wrapped data
    /// </summary>
    /// <param name="insertionData">The wrapped data to insert</param>
    /// <returns>Returns true if creation and insertion was successful, false otherwise</returns>
    public bool Add(TowerDBEntry insertionData){
        if(Check(name)){
            Debug.LogError("TowerDatabase: Detected a duplicate key when attempting to insert!");
            return false;
        }
        towerDB.Add(insertionData.towerName, insertionData);
        return true;
    }

    /// <summary>
    /// Removes an entry from the DB
    /// </summary>
    /// <param name="name">The key to the entry to remove from the BD</param>
    /// <returns>Returns true if removal was successful, false othwerwise</returns>
    public bool Remove(string name){
        if(Check(name)){
            return towerDB.Remove(name);
        }

        return false;
    }

    /// <summary>
    /// Checks if the given tower name is a key in the DB
    /// </summary>
    /// <param name="name">The tower name to check</param>
    /// <returns>Returns true if the tower name is a key in the DB, false otherwise</returns>
    public bool Check(string name){
        if(IsReady() && towerDB.ContainsKey(name)){
            return true;
        }

        return false;
    }

    /// <summary>
    /// Checks if the given wrapped data is a entry in the database
    /// </summary>
    /// <param name="dataWrapper">The wrapped data</param>
    /// <returns>Returns true if the wrapped data corosponds to a entry in the DB, false othwerwise</returns>
    public bool Check(TowerDBEntry dataWrapper){
        if(Check(dataWrapper.name)){
            return towerDB[dataWrapper.name] == dataWrapper.towerGameobject;          
        }

        return false;
    }


    public List<string> GetTowerNames(){
        return new List<string>(towerNames);
    }

    /// <summary>
    /// Has this script finished its startup?
    /// </summary>
    /// <value></value>
    public override bool IsReady(){
        return isReady;
    }
}
