using System;

public class SkinEquipper : IShopItemVisitor
{
    private WeaponSlot _weaponSlot;
    private readonly PetsSpawner _petsSpawner;
    private readonly CharacterSkinChanger _characterSkinChanger;

    public SkinEquipper(CharacterSkinChanger characterSkinChanger, WeaponSlot weaponSlot, PetsSpawner petsSpawner)
    {
        _characterSkinChanger = characterSkinChanger;
        _weaponSlot = weaponSlot;
        _petsSpawner = petsSpawner;
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

            default:
                throw new NotImplementedException();
        }

        //Visit((dynamic)shopItem);
    }

    public void Visit(CharacterSkinItem characterSkinItem) => _characterSkinChanger.Set(characterSkinItem);

    public void Visit(MazeSkinItem mazeSkinItem) => throw new System.NotImplementedException();

    public void Visit(ToolSkinItem toolSkinItem) => _weaponSlot.SetWeapon(toolSkinItem);

    public void Visit(PetSkinItem petSkinItem) => _petsSpawner.SpawnPet(petSkinItem);
}
