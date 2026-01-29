using Invector.vItemManager;
using System.Collections;
using UnityEngine;

public class WeaponEquipper
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
        _characterSkinChanger.StartCoroutine(EquipCoroutine(invectorId));
    }

    private IEnumerator EquipCoroutine(int invectorId)
    {
        yield return null;
        yield return null;
        
        _itemManager.DestroyAllItems();
        
        yield return null;
        yield return null;

        _itemManager.AddItem(new ItemReference(invectorId)
        {
            amount = 1,
            addToEquipArea = true,
        });
    }
}
