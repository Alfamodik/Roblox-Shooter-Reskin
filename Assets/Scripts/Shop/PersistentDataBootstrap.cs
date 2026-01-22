using System;
using UnityEngine;
using YG;

public class PersistentDataBootstrap : MonoBehaviour
{
    [SerializeField] private ShopBootstrap _shopBootstrap;
    [SerializeField] private CharacterSkinChanger _characterSkinChanger;

    private IDataProvider _dataProvider;
    private IPersistentData _persistentPlayerData;

    public void Awake()
    {
        InitializeData();
        _shopBootstrap?.InitializeShop(_persistentPlayerData, _dataProvider);
        InitializeRelatedScripts();
    }

    private void InitializeData()
    {
        _persistentPlayerData = new PersistentData();
        _dataProvider = new DataCloudProvider(_persistentPlayerData);

        LoadDataOrInit();
    }

    private void InitializeRelatedScripts()
    {
        _characterSkinChanger?.Initialize(_persistentPlayerData);
    }
    
    private void LoadDataOrInit()
    {
        if (_dataProvider.TryLoad() == false)
        {
            _persistentPlayerData.PlayerData = new PlayerData();
            YG2.onGetSDKData += SaveProgress;
        }
    }

    private void SaveProgress()
    {
        YG2.onGetSDKData -= SaveProgress;
        _dataProvider.Save();
    }
}
