using TMPro;
using UnityEngine;

public class ObjectiveEntry : MonoBehaviour
{
    public const string Bullet = "●";
    [field: SerializeField]
    public ObjectiveData Objective { get; set; }
    [field: SerializeField]
    public TextMeshProUGUI Label { get; private set; }

    void OnEnable()
    {
        Objective.OnChange += Render;   
    }

    void OnDisable()
    {
        Objective.OnChange -= Render;
    }

    public void Render(ObjectiveData objective)
    {
        Label.text = objective.IsComplete ? $"{Bullet}<s> {objective.Description} </s>" : $"{Bullet} {objective.Description}";
    }
}