using UnityEngine;

[CreateAssetMenu(fileName = "ToolsList", menuName = "Tools/ToolsList")]
public class ToolsList : ScriptableObject
{
    [field: SerializeField] public ToolSkinItem[] Weapons { get; private set; }
}
