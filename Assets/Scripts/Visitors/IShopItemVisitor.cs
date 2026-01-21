public interface IShopItemVisitor
{
    void Visit(ShopItem shopItem);
    void Visit(CharacterSkinItem characterSkinItem);
    void Visit(MazeSkinItem mazeSkinItem);
    void Visit(ToolSkinItem toolSkinItem);
    void Visit(PetSkinItem petSkinItem);
}
