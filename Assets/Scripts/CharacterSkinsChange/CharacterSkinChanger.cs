using Invector.vCharacterController;
using System;
using System.Linq;
using UnityEngine;
using YG;

public class CharacterSkinChanger : MonoBehaviour
{
    public static event Action<vThirdPersonInput> CharacterChanged;

    [SerializeField] private Transform _spawnPoint;

    [field: NonSerialized] public vThirdPersonController CurrentCharacter { get; private set; }

    private IPersistentData _persistentData;
    private ShopContent _shopContent;

    public void Initialize(IPersistentData persistentData, ShopContent shopContent)
    {
        print("CharacterSkinChanger start Initialize");
        _persistentData = persistentData;
        _shopContent = shopContent;
        print("CharacterSkinChanger end Initialize");
    }

    public void Set(CharacterSkinItem characterSkinItem)
    {
        print($"Shop character start");

        bool isFirstSpawn = CurrentCharacter == null;

        print($"Shop character 1");
        Vector3 position = CurrentCharacter != null ? CurrentCharacter.transform.position : _spawnPoint.position;

        print($"Shop character 2");
        Quaternion rotation = CurrentCharacter != null ? CurrentCharacter.transform.rotation : _spawnPoint.rotation;
        
        print($"Shop character 3");
        if (!isFirstSpawn)
        {
            print($"Shop character 4");
            Destroy(CurrentCharacter.gameObject);
        }

        print($"Shop character 5");
        GameObject newCharacter = Instantiate(characterSkinItem.Prefab, position, rotation);

        print($"Shop character 6");
        CurrentCharacter = newCharacter.GetComponent<vThirdPersonController>();
        
        print($"Shop character 7");
        if (YG2.envir.isDesktop || !YG2.envir.isDesktop && !isFirstSpawn)
        {
            print($"Shop character 8");
            InvectorCharacterLinks invectorCharacterLinks = newCharacter.GetComponent<InvectorCharacterLinks>();
            print($"Shop character 9");
            invectorCharacterLinks.MobileUI.SetActive(false);
        }
        
        print($"Shop character 10");
        vThirdPersonInput input = newCharacter.GetComponent<vThirdPersonInput>();

        print($"Shop character 11");
        input.unlockCursorOnStart = YG2.envir.isMobile || !isFirstSpawn;

        print($"Shop character 12");
        input.showCursorOnStart = YG2.envir.isMobile || !isFirstSpawn;
        
        print($"Shop character 13");
        var weaponItem = _shopContent.WeaponSkinItems.FirstOrDefault(w => w.SkinType == _persistentData.PlayerData.SelectedWeaponSkin);
        
        print($"Shop character 14");
        var weaponEquipper = new WeaponEquipper(this);
        
        print($"Shop character 15");
        weaponEquipper.Equip(weaponItem.InvectorId);

        print($"Shop character 16");
        CharacterChanged?.Invoke(input);
        print($"Shop character end");
    }
}
