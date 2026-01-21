using UnityEngine;

public class Axe : MonoBehaviour, ITool
{
    [SerializeField] private string toolType;
    public string ToolType => toolType;

    [SerializeField] private int _coinDevelop;
}
