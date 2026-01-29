using Invector.vCharacterController;
using System;
using System.Linq;
using UnityEngine;

public class CharacterSkinChanger : MonoBehaviour
{
    public static event Action<vThirdPersonInput> CharacterChanged;

    [SerializeField] private Transform _spawnPoint;

    [field: NonSerialized] public vThirdPersonController CurrentCharacter { get; private set; }

    private IPersistentData _persistentData;
    private ShopContent _shopContent;

    public void Initialize(IPersistentData persistentData, ShopContent shopContent)
    {
        _persistentData = persistentData;
        _shopContent = shopContent;
    }

    public void Set(CharacterSkinItem characterSkinItem)
    {
        bool isFirstSpawn = CurrentCharacter == null;
        Vector3 position = CurrentCharacter != null ? CurrentCharacter.transform.position : _spawnPoint.position;
        Quaternion rotation = CurrentCharacter != null ? CurrentCharacter.transform.rotation : _spawnPoint.rotation;
        
        if (!isFirstSpawn)
            Destroy(CurrentCharacter.gameObject);

        GameObject newCharacter = Instantiate(characterSkinItem.Prefab, position, rotation);
        CurrentCharacter = newCharacter.GetComponent<vThirdPersonController>();
        
        vThirdPersonInput input = newCharacter.GetComponent<vThirdPersonInput>();
        input.unlockCursorOnStart = !isFirstSpawn;
        input.showCursorOnStart = !isFirstSpawn;
        
        var weaponItem = _shopContent.WeaponSkinItems.FirstOrDefault(w => w.SkinType == _persistentData.PlayerData.SelectedWeaponSkin);
        var weaponEquipper = new WeaponEquipper(this);
        weaponEquipper.Equip(weaponItem.InvectorId);

        CharacterChanged?.Invoke(input);
    }
}
