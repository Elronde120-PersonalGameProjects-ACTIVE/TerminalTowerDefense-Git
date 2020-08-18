using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class to wrap all important gameplay scripts, and to allow 
/// for a easy way to check if a gameplay script is ready for use
/// </summary>
public abstract class GameplayComponent : MonoBehaviour
{
    /// <summary>
    /// Has this script finished its startup?
    /// </summary>
    /// <value></value>
    public abstract bool IsReady();
}
