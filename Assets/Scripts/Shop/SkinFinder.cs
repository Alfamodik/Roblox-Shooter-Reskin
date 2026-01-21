using System;
using System.Linq;

public class SkinFinder
{
    private ShopItem _shopItem;
    private ShopContent _contentItems;

    public SkinFinder(ShopContent contentItems) => _contentItems = contentItems;

    public ShopItem ShopItem
    {
        get => _shopItem;
        private set
        {
            if (value == null)
                return;

            _shopItem = value;
        }   
    }

    public ShopItem Find(string purchaseId)
    {
        if (string.IsNullOrWhiteSpace(purchaseId))
            throw new ArgumentException($"Skin {nameof(purchaseId)} is empty");

        ShopItem = null;
        ShopItem = _contentItems.CharacterSkinItems.FirstOrDefault(skin => skin.PurchaseId == purchaseId);
        ShopItem = _contentItems.MazeSkinItems.FirstOrDefault(skin => skin.PurchaseId == purchaseId);
        ShopItem = _contentItems.ToolSkinItems.FirstOrDefault(skin => skin.PurchaseId == purchaseId);
        ShopItem = _contentItems.PetSkinItems.FirstOrDefault(skin => skin.PurchaseId == purchaseId);
        return ShopItem;
    }
}