using System;
using UnityEngine;

public class ResourcesHolder : MonoBehaviour
{
    public event Action OnTargetAchieved;

    [field: SerializeField] 
    public ResourceCollector ResourceCollector { get; private set; }

    [field: SerializeField] 
    public int TargetResourcesAmount { get; private set; }
    public int CurrentResourcesAmount { get; private set; }

    private void Awake()
    {
        ResourceCollector.ResourceCollected += OnResourceCollected;
    }

    private void OnDestroy()
    {
        ResourceCollector.ResourceCollected -= OnResourceCollected;
    }

    private void OnResourceCollected(int resourceId)
    {
        CurrentResourcesAmount++;

        if (CurrentResourcesAmount == TargetResourcesAmount)
            OnTargetAchieved?.Invoke();
    }
}