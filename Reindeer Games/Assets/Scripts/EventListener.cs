using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour
{
    [field: SerializeField]
    public ObjectiveListData ObjectiveList { get; private set; }

    public bool HasTriggered = false;
    public UnityEvent OnTriggered;
    private List<ObjectiveData> _observed = new();

    void OnEnable()
    {
        ObjectiveList.OnChange += ObserveObjectives;
        
    }

    private void CheckObjectives(ObjectiveData data)
    {
        if (_observed.All(o => o.IsComplete))
        {
            HasTriggered = true;
            OnTriggered.Invoke();
        }
    }

    void OnDisable()
    {
        ObjectiveList.OnChange -= ObserveObjectives;
        StopObservingObjectives();
        
    }
    
    private void ObserveObjectives(List<ObjectiveData> list)
    {
        StopObservingObjectives();
        _observed = list;
        foreach (var objective in _observed)
        {
            objective.OnChange += CheckObjectives;
        }
    }
    
    private void StopObservingObjectives()
    {
        foreach (var objective in _observed)
        {
            objective.OnChange -= CheckObjectives;
        }
    }
}
