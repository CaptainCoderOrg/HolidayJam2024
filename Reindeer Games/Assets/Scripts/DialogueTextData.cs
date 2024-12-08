using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Game State/Text")]
public class DialogueTextData : ScriptableObject
{
    [SerializeField]
    [TextArea(5, 10)]
    private string _message;
    public string Message => _message;
}