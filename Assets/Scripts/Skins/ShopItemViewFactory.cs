using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemViewFactory", menuName = "Shop/ShopItemViewFactory")]
public class ShopItemViewFactory : ScriptableObject
{
    [SerializeField] private ShopItemView _characterSkinItemPrefab;
    [SerializeField] private ShopItemView _mazeSkinItemPrefab;
    [SerializeField] private ShopItemView _toolSkinItemPrefab;
    [SerializeField] private ShopItemView _petSkinItemPrefab;

    public ShopItemView Get(ShopItem shopItem, Transform parent)
    {
        ShopItemVisitor visitor = new ShopItemVisitor(_characterSkinItemPrefab, _mazeSkinItemPrefab, _toolSkinItemPrefab, _petSkinItemPrefab);
        visitor.Visit(shopItem);

        ShopItemView instance = Instantiate(visitor.Prefab, parent);
        instance.Initialize(shopItem);

        return instance;
    }

    private class ShopItemVisitor : IShopItemVisitor
    {
        private ShopItemView _characterSkinItemPrefab;
        private ShopItemView _mazeSkinItemPrefab;
        private ShopItemView _toolSkinItemPrefab;
        private ShopItemView _petSkinItemPrefab;

        public ShopItemVisitor(ShopItemView characterSkinItemPrefab, ShopItemView mazeSkinItemPrefab, ShopItemView toolSkinItemPrefab, ShopItemView petSkinItemPrefab)
        {
            _characterSkinItemPrefab = characterSkinItemPrefab;
            _mazeSkinItemPrefab = mazeSkinItemPrefab;
            _toolSkinItemPrefab = toolSkinItemPrefab;
            _petSkinItemPrefab = petSkinItemPrefab;
        }

        public ShopItemView Prefab { get; private set; }

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

        public void Visit(CharacterSkinItem characterSkinItem) => Prefab = _characterSkinItemPrefab;

        public void Visit(MazeSkinItem mazeSkinItem) => Prefab = _mazeSkinItemPrefab;

        public void Visit(ToolSkinItem mazeSkinItem) => Prefab = _toolSkinItemPrefab;

        public void Visit(PetSkinItem mazeSkinItem) => Prefab = _petSkinItemPrefab;
    }
}
