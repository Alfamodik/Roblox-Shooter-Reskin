using System;

public class SkinEquipper : IShopItemVisitor
{
    private WeaponSlot _weaponSlot;
    private readonly CharacterSkinChanger _characterSkinChanger;
    private readonly WeaponEquipper _weaponEquipper;

    public SkinEquipper(CharacterSkinChanger characterSkinChanger, WeaponSlot weaponSlot)
    {
        _characterSkinChanger = characterSkinChanger;
        _weaponSlot = weaponSlot;
        _weaponEquipper = new(characterSkinChanger);
    }

    public void Reinitialize(WeaponSlot weaponSlot)
    {
        _weaponSlot = weaponSlot;
    }

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

            case WeaponSkinItem:
                Visit(shopItem as WeaponSkinItem);
                break;

            default:
                throw new NotImplementedException();
        }

        //Visit((dynamic)shopItem);
    }

    public void Visit(CharacterSkinItem characterSkinItem) => _characterSkinChanger.Set(characterSkinItem);

    public void Visit(MazeSkinItem mazeSkinItem) => throw new NotImplementedException();

    public void Visit(ToolSkinItem toolSkinItem) => throw new NotImplementedException();

    public void Visit(PetSkinItem petSkinItem) => throw new NotImplementedException();

    public void Visit(WeaponSkinItem weaponSkinItem) => _weaponEquipper.Equip(weaponSkinItem.InvectorId);
}
