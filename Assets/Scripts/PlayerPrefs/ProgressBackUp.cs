using UnityEngine;

public class ProgressBackUp : MonoBehaviour
{
    private string _currentVersion;
    private string _currentWeapon;
    private string _receivedWeapons;

    private int _rank;
    private int _score;
    private int _pointToRankUpgrade;

    public void CreateBackUp()
    {
        _currentVersion = PlayerPrefs.GetString(nameof(PlayerPrefsKeys.CurrentVersion), "");
        _currentWeapon = PlayerPrefs.GetString(nameof(PlayerPrefsKeys.CurrentWeapon), $"{Weapons.HandGun}");
        _receivedWeapons = PlayerPrefs.GetString(nameof(PlayerPrefsKeys.ReceivedWeapons), "");
        
        _rank = PlayerPrefs.GetInt(nameof(PlayerPrefsKeys.Rank), 0);
        _score = PlayerPrefs.GetInt(nameof(PlayerPrefsKeys.Score), 0);
        _pointToRankUpgrade = PlayerPrefs.GetInt(nameof(PlayerPrefsKeys.PointToRankUpgrade), 5);

    }

    public void LoadBackUp()
    {
        PlayerPrefs.SetString(nameof(PlayerPrefsKeys.CurrentVersion), _currentVersion);
        PlayerPrefs.SetString(nameof(PlayerPrefsKeys.CurrentWeapon), _currentWeapon);
        PlayerPrefs.SetString(nameof(PlayerPrefsKeys.ReceivedWeapons), _receivedWeapons);

        PlayerPrefs.SetInt("score", _rank);
        PlayerPrefs.SetInt("pointToRankUpgrade", _score);
        PlayerPrefs.SetInt("rank", _pointToRankUpgrade);
    }
}
