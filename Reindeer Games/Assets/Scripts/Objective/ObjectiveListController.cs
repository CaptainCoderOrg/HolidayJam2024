using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveListController : MonoBehaviour 
{
    [field: SerializeField]
    public ObjectiveListData ObjectiveList { get; private set; }
    [field: SerializeField]
    public ObjectiveEntry EntryPrefab { get; private set; }
    [field: SerializeField]
    public Transform EntryParent { get; private set; }

    private List<ObjectiveData> _objectives;
    [field: SerializeField]
    public bool IsDirty { get; private set; } = true;

    void OnEnable()
    {
        ObjectiveList.OnChange += MarkDirty;
    }

    void OnDisable()
    {
        ObjectiveList.OnChange -= MarkDirty;
    }

    void Update()
    {
        if (!IsDirty) { return; }
        RenderList(_objectives);
    }

    private void RenderList(List<ObjectiveData> objectives)
    {
        EntryParent.DestroyAllChildren();
        EntryPrefab.gameObject.SetActive(false);
        foreach (ObjectiveData objective in objectives)
        {
            ObjectiveEntry entry = Object.Instantiate(EntryPrefab, EntryParent);
            entry.Objective = objective;
            entry.gameObject.SetActive(true);
        }
        IsDirty = false;
    }

    public void MarkDirty(List<ObjectiveData> objectives)
    {
        _objectives = objectives;
        IsDirty = true;
    }

}