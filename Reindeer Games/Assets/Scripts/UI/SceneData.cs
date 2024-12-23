using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class SceneData : ScriptableObject
{
    public void SwitchScene(string sceneName) => CoroutineRunner.ExecuteCoroutine(SwitchSceneAsync(sceneName));
    private IEnumerator SwitchSceneAsync(string sceneName)
    {
        yield return new WaitForSeconds(1);
        AsyncOperation loading = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!loading.isDone)
        {
            yield return null;
        }
    }
}