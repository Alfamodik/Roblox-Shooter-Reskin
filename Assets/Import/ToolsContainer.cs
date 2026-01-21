using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//using YG;

public class ToolsContainer : MonoBehaviour
{
    [SerializeField] private ToolsList _toolsList;
    public IReadOnlyList<ToolSkinItem> Weapons => Array.AsReadOnly(_toolsList.Weapons);

    private Dictionary<ToolSkinItem, bool> _wosGet;

    private void Start()
    {
        _wosGet = new Dictionary<ToolSkinItem, bool>();

        for (var index = 0; index < _toolsList.Weapons.Length; index++)
        {
            var weapon = _toolsList.Weapons[index];
            _wosGet.Add(weapon, false);
        }

        LoadWeaponBayStates();
    }

    public BayInfo TryBay(int weaponIndex)
    {
        var currentWeapon = _toolsList.Weapons[weaponIndex];

        if (currentWeapon.MethodObtainingSkin != MethodObtainingSkin.Money)
        {
            SaveBayWeapon(weaponIndex);
            return new BayInfo(true, 0);
        }

        if (Wallet.Instance.Balance >= currentWeapon.Price)
        {
            SaveBayWeapon(weaponIndex);
            Wallet.Instance.Spend(currentWeapon.Price);
            return new BayInfo(true, currentWeapon.Price);
        }

        return new BayInfo(false, currentWeapon.Price);
    }

    public bool GetBayStateWeapon(int index)
    {
        if (_toolsList.Weapons[index].MethodObtainingSkin == MethodObtainingSkin.None)
            return true;

        return _wosGet[_toolsList.Weapons[index]];
    }

    private void SaveBayWeapon(int weaponIndex)
    {
        _wosGet[_toolsList.Weapons[weaponIndex]] = true;
        SaveWeaponBayStates();
    }

    public void GetAllWeapon()
    {
        foreach (var weapon in _wosGet.Keys.ToList())
            _wosGet[weapon] = true;

        SaveWeaponBayStates();
    }

    private void SaveWeaponBayStates()
    {
        byte[] bytes = new byte[4];
        bool[] flags = _wosGet.Values.ToArray();
        BitArray bitArray = new(flags);
        bitArray.CopyTo(bytes, 0);

        int packedStates = BitConverter.ToInt32(bytes, 0);
        PlayerPrefs.SetInt("packedWeaponBayStates", packedStates);
        PlayerPrefs.Save();
        //YG2.saves.packedWeaponBayStates = BitConverter.ToInt32(bytes, 0);
        //YG2.SaveProgress();
    }

    private void LoadWeaponBayStates()
    {
        bool[] flags = new bool[_wosGet.Count];
        int packedStates = PlayerPrefs.GetInt("packedWeaponBayStates", 0);
        byte[] bytes = BitConverter.GetBytes(packedStates);
        //byte[] bytes = BitConverter.GetBytes(YG2.saves.packedWeaponBayStates);
        BitArray bitArray = new(bytes);

        for (int i = 0; i < flags.Length; i++)
            flags[i] = i < bitArray.Length && bitArray[i];

        ToolSkinItem[] keys = _wosGet.Keys.ToArray();

        for (int i = 0; i < keys.Length; i++)
            _wosGet[keys[i]] = flags[i];
    }
}

public class BayInfo
{
    public bool State;
    public int Value;

    public BayInfo(bool state, int value)
    {
        State = state;
        Value = value;
    }
}

/*namespace YG
{
    public partial class SavesYG
    {
        public int packedWeaponBayStates;
    }
}*/
