using System.Collections;
using UnityEngine;

public class AspectController : MonoBehaviour 
{
    private float _height = 0;
    private float _width = 0;
    [field: SerializeField]
    public float TargetAspect { get; set;} = 16f/9f;

    void Start()
    {
        Invoke(nameof(Resize), 0.1f);
    }

    public void Update()
    {
        if (_height == Screen.height && _width == Screen.width) { return; }
        Resize();
    }

    public void Resize()
    {
        _height = Screen.height;
        _width = Screen.width;

        Debug.Log("Resizing Camera");
        float scaleHeight = (_width/_height) / TargetAspect;
 
        if (scaleHeight < 1.0f)
        {
            Rect rect = Camera.main.rect;
 
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
 
            Camera.main.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;
 
            Rect rect = Camera.main.rect;
 
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
 
            Camera.main.rect = rect;
        }
    }
    
}