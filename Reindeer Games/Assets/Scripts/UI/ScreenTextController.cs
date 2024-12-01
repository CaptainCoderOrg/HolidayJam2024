using TMPro;
using UnityEngine;

public class ScreenTextController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _shadowLabel;
    [SerializeField]
    private TextMeshProUGUI _textLabel;
    [SerializeField]
    private ScreenTextData _screenText;

    void OnEnable()
    {
        _screenText.OnChange += DisplayText;
    }

    void OnDisable()
    {
        _screenText.OnChange -= DisplayText;
    }

    #if UNITY_EDITOR
    [SerializeField]
    private string _previewText;

    void OnValidate()
    {
        DisplayText(_previewText);
    }
    #endif

    public void DisplayText(string text)
    {
        _shadowLabel.text = text;
        _textLabel.text = text;
    }

}
