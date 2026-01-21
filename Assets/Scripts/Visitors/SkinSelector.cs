using System;

public class SkinSelector : IShopItemVisitor
{
    private IPersistentData _persistentData;

    public SkinSelector(IPersistentData persistentData) => _persistentData = persistentData;

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
        => _persistentData.PlayerData.SelectedCharacterSkin = characterSkinItem.SkinType;

    public void Visit(MazeSkinItem mazeSkinItem) 
        => _persistentData.PlayerData.SelectedMazeSkin = mazeSkinItem.SkinType;

    public void Visit(ToolSkinItem toolSkinItem)
        => _persistentData.PlayerData.SelectedToolSkin = toolSkinItem.SkinType;

    public void Visit(PetSkinItem petSkinItem)
        => _persistentData.PlayerData.SelectedPetSkin = petSkinItem.SkinType;
}
