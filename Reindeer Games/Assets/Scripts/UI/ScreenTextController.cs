using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTextController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _shadowLabel;
    [SerializeField]
    private TextMeshProUGUI _textLabel;
    [SerializeField]
    private ScreenTextData _screenText;
    [SerializeField]
    private Showable _textPanel;

    public float Delay = 0;
    public float DisplayDuration = 0;

    void OnEnable()
    {
        _screenText.OnChange += DisplayText;
    }

    void OnDisable()
    {
        _screenText.OnChange -= DisplayText;
    }

    public void DisplayText(string text)
    {
        if (text == string.Empty)
        {
            _textPanel.gameObject.SetActive(false);
            return;
        }
        _textPanel.Show();
        _shadowLabel.text = text;
        _textLabel.text = text;
        if (Delay > 0)
        {
            StopAllCoroutines();
            StartCoroutine(SlowWriteText(text));
        }
        else
        {
            _textPanel.Show();
            StopAllCoroutines();
            StartCoroutine(ClearTextAfter(DisplayDuration));
        }
    }

    private IEnumerator SlowWriteText(string text)
    {
        var delay = new WaitForSeconds(Delay);
        for (int i = 0; i <= text.Length; i++)
        {
            _shadowLabel.maxVisibleCharacters = i;
            _textLabel.maxVisibleCharacters = i;
            yield return delay;
        }
        StartCoroutine(ClearTextAfter(DisplayDuration));
    }

    private IEnumerator ClearTextAfter(float delay)
    {
        if (delay == 0) { yield break; }
        yield return new WaitForSeconds(delay);
        _textPanel.gameObject.SetActive(false);
    }

    #if UNITY_EDITOR
    [SerializeField]
    private string _previewText;

    void OnValidate()
    {
        _shadowLabel.text = _previewText;
        _textLabel.text = _previewText;
    }
    #endif

}