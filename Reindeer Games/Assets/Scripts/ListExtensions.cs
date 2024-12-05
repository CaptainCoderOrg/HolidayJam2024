using System.Collections.Generic;

public static class ListExtensions
{
    public static T GetAtOrDefault<T>(this List<T> values, int ix)
    {
        if (values.Count > ix) { return values[ix]; }
        return default;
    }
}