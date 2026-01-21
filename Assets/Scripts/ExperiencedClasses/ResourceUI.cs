using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] private ResourcesHolder experienceHolder;
    [SerializeField] private Slider resourceSlider;

    private void Update()
    {
        resourceSlider.maxValue = experienceHolder.TargetResourcesAmount;

        int collected = experienceHolder.CurrentResourcesAmount;
        resourceSlider.value = collected;

        float percentage = experienceHolder.TargetResourcesAmount > 0 ? (float)collected / experienceHolder.TargetResourcesAmount * 100f : 0f;
        Debug.Log($"EnemyResourceUI: collected={collected}/{experienceHolder.TargetResourcesAmount} ({percentage:F1}%)");
    }
}