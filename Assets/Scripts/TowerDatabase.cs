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
    private Dictionary<string, GameObject> towerDB = new Dictionary<string, GameObject>();

    [SerializeField]
    private NameTowerPair[] initialTowerDatabase;
    
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
        }
        isReady = true;
    }

    /// <summary>
    /// Gets a tower gameobject from the database
    /// </summary>
    /// <param name="name">The name of the tower to get</param>
    /// <returns>Returns a valid gameobject of name corosponds to a entry in the DB, null otherwise</returns>
    public GameObject Get(string name){
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
    /// Creates a new entry in the DB based on given data
    /// </summary>
    /// <param name="name">The name of the tower (also the key in the DB)</param>
    /// <param name="tower">The tower gameobject (also the value in the DB</param>
    /// <returns>Returns true if creation was successful, false otherwise</returns>
    public bool Add(string name, GameObject tower){
        if(Check(name)){
            Debug.LogError("TowerDatabase: Detected a duplicate key when attempting to insert!");
            return false;
        }
        towerDB.Add(name,tower);
        return true;
    }

    /// <summary>
    /// Creates a new entry in the DB based on given wrapped data
    /// </summary>
    /// <param name="insertionData">The wrapped data to insert</param>
    /// <returns>Returns true if creation and insertion was successful, false otherwise</returns>
    public bool Add(NameTowerPair insertionData){
        return Add(insertionData.name, insertionData.tower);
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
    public bool Check(NameTowerPair dataWrapper){
        if(Check(dataWrapper.name)){
            return towerDB[dataWrapper.name] == dataWrapper.tower;          
        }

        return false;
    }

    /// <summary>
    /// Has this script finished its startup?
    /// </summary>
    /// <value></value>
    public override bool IsReady(){
        return isReady;
    }

    /// <summary>
    /// Class made for easy wrapping and data input for database creation
    /// </summary>
    [System.Serializable]
    public class NameTowerPair{
        public string name;
        public GameObject tower;
    };
}
