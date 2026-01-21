using System;

public class SkinUnlocker : IShopItemVisitor
{
    private IPersistentData _persistentData;

    public SkinUnlocker(IPersistentData persistentData) => _persistentData = persistentData;

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
        => _persistentData.PlayerData.OpenCharacterSkin(characterSkinItem.SkinType);

    public void Visit(MazeSkinItem mazeSkinItem) 
        => _persistentData.PlayerData.OpenMazeSkin(mazeSkinItem.SkinType);

    public void Visit(ToolSkinItem toolSkinItem)
        => _persistentData.PlayerData.OpenToolSkin(toolSkinItem.SkinType);

    public void Visit(PetSkinItem peteSkinItem)
        => _persistentData.PlayerData.OpenPetSkin(peteSkinItem.SkinType);
}
