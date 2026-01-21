using System;
using System.Linq;
using UnityEngine;

public class CharacterSkinChanger : MonoBehaviour
{
    public event Action<Animator> AnimatorChanged;
    public event Action<WeaponSlot> WeaponSlotChanged;

    [SerializeField] private Transform _parent;
    [SerializeField] private Transform _spawnPoint;

    [Space]
    [SerializeField] private ToolsContainer _toolsContainer;
    [SerializeField] private GameObject _characterSkin;

    [SerializeField] private ShopContent _shopContent;

    private IPersistentData _iPersistentData;

    private void Start()
    {
        LoadSkin();
    }

    public void Initialize(IPersistentData iPersistentData)
    {
        _iPersistentData = iPersistentData;
    }

    public void Set(CharacterSkinItem characterSkinItem)
    {
        Destroy(_characterSkin);
        _characterSkin = Spawn(characterSkinItem);

        Animator animator = _characterSkin.GetComponent<Animator>();
        WeaponSlot weaponSlot = _characterSkin.GetComponentInChildren<WeaponSlot>();

        weaponSlot.Initialize(_toolsContainer);

        AnimatorChanged?.Invoke(animator);
        WeaponSlotChanged?.Invoke(weaponSlot);
    }

    public GameObject Spawn(CharacterSkinItem characterSkinItem)
        => SpawnPrefab(characterSkinItem.Prefab);

    private GameObject SpawnPrefab(GameObject gameObject)
        => Instantiate(gameObject, _spawnPoint.position, new Quaternion(0, 0, 0, 0), _parent);

    private void LoadSkin()
    {
        CharacterSkinItem characterSkinItem = _shopContent.CharacterSkinItems
            .FirstOrDefault(skin => skin.SkinType == _iPersistentData.PlayerData.SelectedCharacterSkin);

        Set(characterSkinItem);
    }
}
