using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ToolData", menuName = "Tools/ToolData")]
public class ToolData : ScriptableObject
{
    public int toolLevel = 1;
    public List<ResourceType> mineableResources;
    public float miningTime;
}

public enum ResourceType
{
    Wood,
    Iron,
    Gold,
}
