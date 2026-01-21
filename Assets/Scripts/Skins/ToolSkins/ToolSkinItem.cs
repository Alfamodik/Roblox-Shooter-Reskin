using UnityEngine;

[CreateAssetMenu(fileName = "ToolSkinItem", menuName = "Shop/ToolSkinItem")]
public class ToolSkinItem : ShopItem
{
    [field: SerializeField] public ToolSkins SkinType { get; private set; }
}
