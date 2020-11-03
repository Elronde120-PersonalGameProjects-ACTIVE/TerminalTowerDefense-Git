using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class that contains all relevant data about an AI Agent Type
/// </summary>
[CreateAssetMenu(fileName = "New AI Data", menuName = "ScriptableObjects/AI/AI Data", order = 0)]
public class AIData : ScriptableObject {
    public string agentName = "Default AI Name";
    public float moveSpeed = 1f;
    public float playerBaseDamage;
    public int navigationTickRate; //how many ticks before the AI can move
    public Resources resourcesToGiveOnDeath;

}
