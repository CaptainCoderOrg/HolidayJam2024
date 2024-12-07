using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions
{
    public static T GetAtOrDefault<T>(this List<T> values, int ix)
    {
        if (values.Count > ix) { return values[ix]; }
        return default;
    }

    public static T GetRandom<T>(this List<T> values) => values[Random.Range(0, values.Count)];
}