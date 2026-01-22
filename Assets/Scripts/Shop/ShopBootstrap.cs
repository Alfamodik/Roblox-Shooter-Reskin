using UnityEngine;
using YG;

public class ShopBootstrap : MonoBehaviour
{
    [SerializeField] private Shop _shop;
    [SerializeField] private WalletView _walletView;
    private SkinUnlocker _skinUnlocker;

    public void InitializeShop(IPersistentData persistentData, IDataProvider dataCloudProvider)
    {
        _skinUnlocker = new SkinUnlocker(persistentData);

        OpenSkinsChecker openSkinsChecker = new OpenSkinsChecker(persistentData);
        SelectedSkinChecker selectedSkinChecker = new SelectedSkinChecker(persistentData);
        SkinSelector skinSelector = new SkinSelector(persistentData);

        _shop.Initialize(dataCloudProvider, openSkinsChecker, selectedSkinChecker, skinSelector, _skinUnlocker);
        YG2.ConsumePurchases();
    }
}
