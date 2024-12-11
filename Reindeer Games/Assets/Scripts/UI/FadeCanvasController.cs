using UnityEngine;

public class FadeCanvasController : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    public SceneData _sceneData;

    public void FadeIn() => _animator.SetTrigger("FadeIn");
    public void FadeOut() => _animator.SetTrigger("FadeOut");
}