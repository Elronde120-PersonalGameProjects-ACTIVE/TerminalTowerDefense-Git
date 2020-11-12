using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static bool IsValidIndex(int index, ICollection collection) => index < collection.Count && index >= 0;
}
