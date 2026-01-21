using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
//using YG;

public class Wallet : MonoBehaviour
{
    [HideInInspector] public UnityEvent<int> BalanceChanged;

    [SerializeField] private bool _setDefaultSavesOnAwake;

    public static Wallet Instance { get; private set; }
    public int Balance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Initialize();
    }

    public void Add(int amount, bool playSound = true, string soundName = "GettingMoney")
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        Balance += amount;
        
        Save();
        UpdateLeaderboardReceivedMoney(amount);

        BalanceChanged.Invoke(Balance);

        if (playSound)
            SFXProvider.Play(soundName);
    }

    public void Spend(int amount, bool playSound = true, string soundName = "SpendMoney")
    {
        if (amount < 0 || Balance < amount)
            throw new ArgumentOutOfRangeException(nameof(amount));

        Balance -= amount;
        Save();

        BalanceChanged.Invoke(Balance);

        if(playSound)
            SFXProvider.Play(soundName);
    }

    public bool IsEnough(int coins)
    {
        if (coins < 0)
            throw new ArgumentOutOfRangeException(nameof(coins));

        return Balance >= coins;
    }

    private void Initialize()
    {
/*#if UNITY_EDITOR
        if (_setDefaultSavesOnAwake)
        {
            if (YG2.isSDKEnabled)
                SetDefaultSaves();
            else
                StartCoroutine(SetDefaultSavesAfterSdkInit());
        }
#endif*/

        Instance = this;
        DontDestroyOnLoad(gameObject);

        BalanceChanged = new UnityEvent<int>();
        Balance = PlayerPrefs.GetInt("Balance", 0);
    }

    private void Save()
    {
        PlayerPrefs.SetInt("Balance", Balance);
        //YG2.saves.Balance = Balance;
        //YG2.SetLeaderboard("CurrentMoney", Balance);

        if (PlayerPrefs.GetInt("HighestBalance") < Balance)
        {
            PlayerPrefs.SetInt("HighestBalance", Balance);
            //YG2.SetLeaderboard("MoneyTop", Balance);
        }
        
        PlayerPrefs.Save();
        
        //if (YG2.isSDKEnabled)
        //    YG2.SaveProgress();
    }

    private void UpdateLeaderboardReceivedMoney(int receivedMoney)
    {
        int moneyReceived = PlayerPrefs.GetInt("MoneyReceived") + receivedMoney;
        PlayerPrefs.SetInt("MoneyReceived", moneyReceived);
        //YG2.SetLeaderboard("MoneyReceived", moneyReceived);

        //if (YG2.isSDKEnabled)
        //    YG2.SaveProgress();
    }

/*    private void SetDefaultSaves()
    {
        YG2.SetDefaultSaves();
        YG2.SaveProgress();
    }

    private IEnumerator SetDefaultSavesAfterSdkInit()
    {
        yield return new WaitUntil(() => YG2.isSDKEnabled);
        SetDefaultSaves();
    }*/
}

/*namespace YG
{
    public partial class SavesYG
    {
        public int Balance;
    }
}*/
