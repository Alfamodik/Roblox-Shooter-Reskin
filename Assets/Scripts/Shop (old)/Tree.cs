using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour, IResource
{
    public List<string> allowedTools;

    public bool Interact(ITool tool)
    {
        return allowedTools.Contains(tool.ToolType);
    }
}
