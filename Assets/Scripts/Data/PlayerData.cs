using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    private CharacterSkins _selectedCharacterSkin;
    private MazeSkins _selectedMazeSkin;
    private ToolSkins _selectedToolSkin;
    private PetSkins _selectedPetSkin;

    private readonly List<CharacterSkins> _openCharacterSkins;
    private readonly List<MazeSkins> _openMazeSkins;
    private readonly List<ToolSkins> _openToolSkins;
    private readonly List<PetSkins> _openPetSkins;

    public PlayerData(bool setDefaultValues = true)
    {
        _selectedCharacterSkin = CharacterSkins.Man1;
        _selectedMazeSkin = MazeSkins.Green;
        _selectedToolSkin = ToolSkins.LightweightAxe;
        //_selectedPetSkin = PetSkins.BabyDragon1;

        _openCharacterSkins = new List<CharacterSkins>();
        _openMazeSkins = new List<MazeSkins>();
        _openToolSkins = new List<ToolSkins>();
        _openPetSkins = new List<PetSkins>();

        if (setDefaultValues)
        {
            _openCharacterSkins.Add(_selectedCharacterSkin);
            _openMazeSkins.Add(_selectedMazeSkin);
            _openToolSkins.Add(_selectedToolSkin);
            //_openPetSkins.Add(_selectedPetSkin);
        }
    }

    public CharacterSkins SelectedCharacterSkin
    {
        get => _selectedCharacterSkin;
        set
        {
            if (_openCharacterSkins.Contains(value) == false)
                throw new ArgumentException(nameof(value));

            _selectedCharacterSkin = value;
        }
    }

    public MazeSkins SelectedMazeSkin
    {
        get => _selectedMazeSkin;
        set
        {
            if (_openMazeSkins.Contains(value) == false)
                throw new ArgumentException(nameof(value));

            _selectedMazeSkin = value;
        }
    }

    public ToolSkins SelectedToolSkin
    {
        get => _selectedToolSkin;
        set
        {
            if (_openToolSkins.Contains(value) == false)
                throw new ArgumentException(nameof(value));

            _selectedToolSkin = value;
        }
    }

    public PetSkins SelectedPetSkin
    {
        get => _selectedPetSkin;
        set
        {
            if (_openPetSkins.Contains(value) == false)
                throw new ArgumentException(nameof(value));

            _selectedPetSkin = value;
        }
    }

    public IEnumerable<CharacterSkins> OpenCharacterSkins => _openCharacterSkins;

    public IEnumerable<MazeSkins> OpenMazeSkins => _openMazeSkins;

    public IEnumerable<ToolSkins> OpenToolSkins => _openToolSkins;

    public IEnumerable<PetSkins> OpenPetSkins => _openPetSkins;

    public void OpenCharacterSkin(CharacterSkins skin)
    {
        if(_openCharacterSkins.Contains(skin))
            Debug.LogWarning($"Dublicate {nameof(skin)}: {skin} in {nameof(_openCharacterSkins)}");
            //throw new ArgumentException(nameof(skin));

        _openCharacterSkins.Add(skin);
    }

    public void OpenMazeSkin(MazeSkins skin)
    {
        if (_openMazeSkins.Contains(skin))
            Debug.LogWarning($"Dublicate {nameof(skin)}: {skin} in {nameof(_openMazeSkins)}");
            //throw new ArgumentException(nameof(skin));

        _openMazeSkins.Add(skin);
    }

    public void OpenToolSkin(ToolSkins skin)
    {
        if (_openToolSkins.Contains(skin))
            Debug.LogWarning($"Dublicate {nameof(skin)}: {skin} in {nameof(_openToolSkins)}");
            //throw new ArgumentException(nameof(skin));

        _openToolSkins.Add(skin);
    }

    public void OpenPetSkin(PetSkins skin)
    {
        if (_openPetSkins.Contains(skin))
            Debug.LogWarning($"Dublicate {nameof(skin)}: {skin} in {nameof(_openPetSkins)}");
            //throw new ArgumentException(nameof(skin));

        _openPetSkins.Add(skin);
    }
}
