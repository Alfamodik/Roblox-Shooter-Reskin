using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDataProvider
{
    public const string WeaponDataKey = nameof(PlayerPrefsKeys.ReceivedWeapons);

    private static Dictionary<Weapon, bool> _savedData = new();

    public static bool TryGet(Weapon weapon, out bool isUnlocked)
    {
        Load();
        return _savedData.TryGetValue(weapon, out isUnlocked);
    }

    public static void Save(Weapon weapon, bool value)
    {
        Load();

        if(_savedData.ContainsKey(weapon) == false)
            _savedData.Add(weapon, value);

        _savedData[weapon] = value;
        PlayerPrefs.SetString(WeaponDataKey, JsonConvert.SerializeObject(_savedData));
    }

    private static void Load()
    {
        _savedData = JsonConvert.DeserializeObject<Dictionary<Weapon, bool>>(
            PlayerPrefs.GetString(WeaponDataKey));

        _savedData ??= new Dictionary<Weapon, bool>();
    }
}
