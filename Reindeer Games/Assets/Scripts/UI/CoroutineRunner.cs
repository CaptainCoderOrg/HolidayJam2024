using System.Collections;
using System.Threading;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
    public static void ExecuteCoroutine(IEnumerator coroutine)
    {
        GameObject go = new GameObject();
        var runner = go.AddComponent<CoroutineRunner>();
        runner.StartCoroutine(coroutine);
    }

    public void RunCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(RunThenDestroy(coroutine));
    }

    private IEnumerator RunThenDestroy(IEnumerator coroutine)
    {
        yield return StartCoroutine(coroutine);
        Object.Destroy(this.gameObject);
    }
}