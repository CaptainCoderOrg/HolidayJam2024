using UnityEngine;
using UnityEngine.EventSystems;

public class TitleScreenController : MonoBehaviour
{
    public void ClearSelection()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
