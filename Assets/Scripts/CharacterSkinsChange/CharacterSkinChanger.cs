using Invector.vCharacterController;
using System;
using System.Linq;
using UnityEngine;

public class CharacterSkinChanger : MonoBehaviour
{
    public static event Action<vThirdPersonInput> CharacterChanged;
    
    [field: SerializeField] public vThirdPersonController CurrentCharacter { get; private set; }

    private IPersistentData _persistentData;
    private ShopContent _shopContent;

    public void Initialize(IPersistentData persistentData, ShopContent shopContent)
    {
        _persistentData = persistentData;
        _shopContent = shopContent;
    }

    public void Set(CharacterSkinItem characterSkinItem)
    {
        Vector3 position = CurrentCharacter != null ? CurrentCharacter.transform.position : transform.position;
        Quaternion rotation = CurrentCharacter != null ? CurrentCharacter.transform.rotation : transform.rotation;
        
        if (CurrentCharacter != null)
            Destroy(CurrentCharacter.gameObject);

        GameObject newCharacter = Instantiate(characterSkinItem.Prefab, position, rotation);
        CurrentCharacter = newCharacter.GetComponent<vThirdPersonController>();
        
        vThirdPersonInput input = newCharacter.GetComponent<vThirdPersonInput>();
        input.unlockCursorOnStart = true;
        input.showCursorOnStart = true;
        
        var weaponItem = _shopContent.WeaponSkinItems.FirstOrDefault(w => w.SkinType == _persistentData.PlayerData.SelectedWeaponSkin);
        var weaponEquipper = new WeaponEquipper(this);
        weaponEquipper.Equip(weaponItem.InvectorId);

        CharacterChanged?.Invoke(input);
    }
}
