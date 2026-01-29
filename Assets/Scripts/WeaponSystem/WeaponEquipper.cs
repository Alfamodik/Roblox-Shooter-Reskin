using Invector.vItemManager;
using UnityEngine;

public class WeaponEquipper : MonoBehaviour
{
    private vItemManager _itemManager;
    private CharacterSkinChanger _characterSkinChanger;

    public WeaponEquipper(CharacterSkinChanger characterSkinChanger)
    {
        _characterSkinChanger = characterSkinChanger;
    }

    public void Equip(int invectorId)
    {
        _itemManager = _characterSkinChanger.CurrentCharacter.GetComponent<vItemManager>();

        if (TryEquip(invectorId))
         return;

        _itemManager.AddItem(new ItemReference(invectorId)
        {
            amount = 1,
            addToEquipArea = true,
        });

        TryEquip(invectorId);
    }

    private bool TryEquip(int invectorId)
    {
        foreach (vItem item in _itemManager.inventory.items)
        {
            if (item.id == invectorId)
            {
                _itemManager.EquipItemToEquipSlot(0, 1, item, true);
                return true;
            }
        }

        return false;
    }
}
