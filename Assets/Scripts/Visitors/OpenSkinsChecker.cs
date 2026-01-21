using System;
using System.Linq;

public class OpenSkinsChecker : IShopItemVisitor
{
    private IPersistentData _persistentData;

    public bool IsOpened { get; private set; }

    public OpenSkinsChecker(IPersistentData persistentData) => _persistentData = persistentData;

    public void Visit(ShopItem shopItem)
    {
        switch (shopItem)
        {
            case CharacterSkinItem:
                Visit(shopItem as CharacterSkinItem);
                break;

            case MazeSkinItem:
                Visit(shopItem as MazeSkinItem);
                break;

            case ToolSkinItem:
                Visit(shopItem as ToolSkinItem);
                break;

            case PetSkinItem:
                Visit(shopItem as PetSkinItem);
                break;

            default:
                throw new NotImplementedException();
        }

        //Visit((dynamic)shopItem);
    }

    public void Visit(CharacterSkinItem characterSkinItem) 
        => IsOpened = _persistentData.PlayerData.OpenCharacterSkins.Contains(characterSkinItem.SkinType);

    public void Visit(MazeSkinItem mazeSkinItem)
        => IsOpened = _persistentData.PlayerData.OpenMazeSkins.Contains(mazeSkinItem.SkinType);

    public void Visit(ToolSkinItem toolSkinItem)
        => IsOpened = _persistentData.PlayerData.OpenToolSkins.Contains(toolSkinItem.SkinType);

    public void Visit(PetSkinItem petSkinItem)
        => IsOpened = _persistentData.PlayerData.OpenPetSkins.Contains(petSkinItem.SkinType);
}
