using System;
using UnityEngine;

public class ResourceCollector : MonoBehaviour
{
    public event Action<int> ResourceCollected; 

    public void CollectResource(int resourceId)
    {
        Debug.Log($"ResourceCollector: CollectResource вызван для ресурса {resourceId}");
        
        ResourceCollected?.Invoke(resourceId);

        Debug.Log($"[ResourceCollector] ResourceCollected event вызван");
    }
}
