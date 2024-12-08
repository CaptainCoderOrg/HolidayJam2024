using UnityEngine;

public static class TransformExtensions
{
    public static void DestroyAllChildren(this Transform transform)
    {
        System.Action<Object> destroy = Application.isPlaying ? Object.Destroy : Object.DestroyImmediate;
        for (int ix = transform.childCount -1 ; ix >= 0; ix--)
        {
            destroy.Invoke(transform.GetChild(ix).gameObject);
        }
    }
}