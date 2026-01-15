using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class ReceivedWeaponsForAdvertising : MonoBehaviour
{
    public const string WeaponsPrefName = "WeaponsData";

    private Dictionary<string, bool> _weaponsReceivedData = new Dictionary<string, bool>();

    private void Awake() => LoadData();

    public bool GetData(string name)
    {
        LoadData();

        if (_weaponsReceivedData.ContainsKey(name))
            return _weaponsReceivedData[name];

        _weaponsReceivedData.Add(name, false);
        return false;
    }

    public void WeaponBought(string name)
    {
        if(_weaponsReceivedData.ContainsKey(name) == false)
            _weaponsReceivedData.Add(name, true);

        else
            _weaponsReceivedData[name] = true;

        SaveToPlayerPrefs();
    }

    public void ResetData()
    {
        _weaponsReceivedData.Clear();
        SaveToPlayerPrefs();
    }

    private void LoadData()
    {
        _weaponsReceivedData = JsonConvert.DeserializeObject<Dictionary<string, bool>>(PlayerPrefs.GetString(WeaponsPrefName));

        if(_weaponsReceivedData == null)
            _weaponsReceivedData = new Dictionary<string, bool>();
    }

    private void SaveToPlayerPrefs() => PlayerPrefs.SetString(WeaponsPrefName, JsonConvert.SerializeObject(_weaponsReceivedData));
}
