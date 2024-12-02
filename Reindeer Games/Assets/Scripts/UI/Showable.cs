using UnityEngine;
using UnityEngine.Events;

public class Showable : MonoBehaviour
{
    public UnityEvent OnShow;

    public void Show()
    {
        gameObject.SetActive(true);
        OnShow.Invoke();
    }

}