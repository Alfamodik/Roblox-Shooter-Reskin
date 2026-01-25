using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopContent", menuName = "Shop/ShopContent")]
public class ShopContent : ScriptableObject
{
    [SerializeField] private List<CharacterSkinItem> _characterSkinItems;
    [SerializeField] private List<MazeSkinItem> _mazeSkinItems;
    [SerializeField] private List<ToolSkinItem> _toolSkinItems;
    [SerializeField] private List<WeaponSkinItem> _weaponSkinItems;
    [SerializeField] private List<PetSkinItem> _petSkinItems;

    public IEnumerable<CharacterSkinItem> CharacterSkinItems => _characterSkinItems;
    public IEnumerable<MazeSkinItem> MazeSkinItems => _mazeSkinItems;
    public IEnumerable<ToolSkinItem> ToolSkinItems => _toolSkinItems;
    public IEnumerable<WeaponSkinItem> WeaponSkinItems => _weaponSkinItems;
    public IEnumerable<PetSkinItem> PetSkinItems => _petSkinItems;

    private void OnValidate()
    {
        var charaterSkinsDuplicates = _characterSkinItems.GroupBy(item => item.SkinType)
            .Where(array => array.Count() > 1);

        if (charaterSkinsDuplicates.Count() > 0)
            throw new InvalidOperationException(nameof(_characterSkinItems));

        var mazeSkinsDuplicates = _mazeSkinItems.GroupBy(item => item.SkinType)
            .Where(array => array.Count() > 1);

        if (mazeSkinsDuplicates.Count() > 0)
            throw new InvalidOperationException(nameof(_mazeSkinItems));

        var toolSkinsDuplicates = _toolSkinItems.GroupBy(item => item.SkinType)
            .Where(array => array.Count() > 1);

        if (toolSkinsDuplicates.Count() > 0)
            throw new InvalidOperationException(nameof(_toolSkinItems));

        var weaponSkinsDuplicates = _weaponSkinItems.GroupBy(item => item.SkinType)
            .Where(array => array.Count() > 1);

        if (weaponSkinsDuplicates.Count() > 0)
            throw new InvalidOperationException(nameof(_weaponSkinItems));

        var petSkinsDuplicates = _petSkinItems.GroupBy(item => item.SkinType)
            .Where(array => array.Count() > 1);

        if (petSkinsDuplicates.Count() > 0)
            throw new InvalidOperationException(nameof(_petSkinItems));
    }
}
