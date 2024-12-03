using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu]
public class SlidingPanelController : MonoBehaviour
{
    private Animator _animator;
    [field: SerializeField]
    public bool IsShowing { get; private set; }

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        if (IsShowing) { Show(); }
    }
    
    public void Toggle()
    {
        if (IsShowing)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    public void Show()
    {
        _animator.SetTrigger("Show");
        IsShowing = true;
    }

    public void Hide()
    {
        _animator.SetTrigger("Hide");
        IsShowing = false;
    }

}