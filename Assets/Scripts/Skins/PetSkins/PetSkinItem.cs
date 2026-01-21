using UnityEngine;

[CreateAssetMenu(fileName = "PetSkinItem", menuName = "Shop/PetSkinItem")]
public class PetSkinItem : ShopItem
{
    [field: SerializeField] public PetSkins SkinType { get; private set; }
}
