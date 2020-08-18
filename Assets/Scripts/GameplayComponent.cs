using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameplayComponent : MonoBehaviour
{
    /// <summary>
    /// Has this script finished its startup?
    /// </summary>
    /// <value></value>
    public abstract bool IsReady();
}
