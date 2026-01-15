using System;
using UnityEngine;
using Invector.vItemManager;

public class EquipWeapon : MonoBehaviour
{
    [SerializeField] private vItemManager _vItemManager;

    public void Equip(Weapon weapon)
    {
        if(TryEquip(weapon))
            return;

        _vItemManager.AddItem(new ItemReference((int)weapon)
        {
            amount = 1,
            addToEquipArea = true,
        });

        print($"Created: {Enum.GetName(typeof(Weapon), weapon)}");
        
        if(TryEquip(weapon))
            return;

        print("Couldn't equip");
    }

    private bool TryEquip(Weapon weapon)
    {
        foreach(vItem item in _vItemManager.inventory.items)
        {
            if(item.id == (int)weapon)
            {
                _vItemManager.EquipItemToEquipSlot(0, 1, item, true);
                print($"Equiped: {item.name}");
                return true;
            }
        }

        return false;
    }
}
