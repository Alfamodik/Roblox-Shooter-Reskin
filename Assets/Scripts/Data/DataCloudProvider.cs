using System.Collections.Generic;
using System.Linq;
//using YG;

public class DataCloudProvider : IDataProvider
{
    private readonly IPersistentData _persistentData;

    public DataCloudProvider(IPersistentData playerData) => _persistentData = playerData;

    private PlayerData PlayerData => _persistentData.PlayerData;

    public void Save()
    {
        /*YG2.saves.SelectedCharacterSkin = (int)PlayerData.SelectedCharacterSkin;
        YG2.saves.SelectedMazeSkin = (int)PlayerData.SelectedMazeSkin;
        YG2.saves.SelectedToolSkin = (int)PlayerData.SelectedToolSkin;
        //YG2.saves.SelectedPetSkin = (int)PlayerData.SelectedPetSkin;

        YG2.saves.OpenCharacterSkins = PlayerData.OpenCharacterSkins.Select(skins => (int)skins).ToList();
        YG2.saves.OpenMazeSkins = PlayerData.OpenMazeSkins.Select(skins => (int)skins).ToList();
        YG2.saves.OpenToolSkins = PlayerData.OpenToolSkins.Select(skins => (int)skins).ToList();
        YG2.saves.OpenPetSkins = PlayerData.OpenPetSkins.Select(skins => (int)skins).ToList();

        YG2.saves.SkinsDataInitialized = true;
        YG2.SaveProgress();*/
    }

    public bool TryLoad()
    {
        /*if (!YG2.saves.SkinsDataInitialized)
            return false;

        _persistentData.PlayerData = new(false);

        foreach (var skin in YG2.saves.OpenCharacterSkins.Select(skins => (CharacterSkins)skins))
            PlayerData.OpenCharacterSkin(skin);

        foreach (var skin in YG2.saves.OpenMazeSkins.Select(skins => (MazeSkins)skins))
            PlayerData.OpenMazeSkin(skin);

        foreach (var skin in YG2.saves.OpenToolSkins.Select(skins => (ToolSkins)skins))
            PlayerData.OpenToolSkin(skin);

        foreach (var skin in YG2.saves.OpenPetSkins.Select(skins => (PetSkins)skins))
            PlayerData.OpenPetSkin(skin);

        PlayerData.SelectedCharacterSkin = (CharacterSkins)YG2.saves.SelectedCharacterSkin;
        PlayerData.SelectedMazeSkin = (MazeSkins)YG2.saves.SelectedMazeSkin;
        PlayerData.SelectedToolSkin = (ToolSkins)YG2.saves.SelectedToolSkin;
        //PlayerData.SelectedPetSkin = (PetSkins)YG2.saves.SelectedPetSkin;

        return true;*/
        return false;
    }
}

/*namespace YG
{
    public partial class SavesYG
    {
        public bool SkinsDataInitialized;

        public int SelectedCharacterSkin;
        public int SelectedMazeSkin;
        public int SelectedToolSkin;
        public int SelectedPetSkin;
        
        public List<int> OpenCharacterSkins;
        public List<int> OpenMazeSkins;
        public List<int> OpenToolSkins;
        public List<int> OpenPetSkins;
    }
}*/
