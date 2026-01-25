using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSkinItem", menuName = "Shop/WeaponSkinItem")]
public class WeaponSkinItem : ShopItem
{
    [field: SerializeField] public WeaponSkins SkinType { get; private set; }
}
