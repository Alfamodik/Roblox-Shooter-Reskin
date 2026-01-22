using DG.Tweening;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ShopUiManager : MonoBehaviour
{
    [Header("Shop Settings")]
    [SerializeField] private ToolsContainer _weaponsContainer;
    [SerializeField] private WeaponSlot _weaponSlot;
    [SerializeField] private Image back;
    [SerializeField] private Camera weaponCamera;
    [SerializeField] private Button closeButton;
    [SerializeField] private ThirdPersonController characterThirdPersonController;

    [Header("UI Elements")]
    [SerializeField] private Transform weaponUiSlot;
    [SerializeField] private WeaponSelectUI weaponSelectUIPrefab;
    [SerializeField] private Transform weaponPanel;
    [SerializeField] private ShopGetButton getButton;
    [SerializeField] private TextMeshProUGUI walletBalanceText;

    [SerializeField] private float pressSize;
    [SerializeField] private float pressDuration;

    private GameObject _currentWeaponGameObject;
    private ToolSkinItem _currentWeapons;

    private void Awake()
    {
        YG2.onRewardAdv += GiveWeapon;
        YG2.onPurchaseSuccess += GiveWeapon;
    }

    private void Start()
    {
        ClearChild();
        getButton.tryGet.AddListener(BuyWeapon);
        closeButton.onClick.AddListener(Close);

        if (Wallet.Instance != null)
        {
            Wallet.Instance.BalanceChanged.AddListener(OnBalanceChanged);
        }

        for (int index = 0; index < _weaponsContainer.Weapons.Count; index++)
        {
            var weapon = _weaponsContainer.Weapons[index]; // перебирает все оружия 

            WeaponSelectUI weaponSelectUI = Instantiate(weaponSelectUIPrefab, weaponPanel); //и спаунит в селект ui
            weaponSelectUI.Initialize(weapon.Image);

            var index1 = index;
            weaponSelectUI.onSelect.AddListener((() => SetWeapon(index1)));
        }

        Close(false);
    }

    public void GiveWeapon(string weaponName)
    {
        Debug.Log($"GiveWeapon {weaponName}");

        int weaponIndex = _weaponsContainer.Weapons.ToList().FindIndex(w => w.PurchaseId == weaponName);
        ToolSkinItem weapon = _weaponsContainer.Weapons[weaponIndex];

        _weaponsContainer.TryBay(weaponIndex);
        _weaponSlot.SetWeapon(weaponIndex);

        ToolSaver.SaveCurrentTool(weaponIndex);
        getButton.Enabled(weaponIndex, _weaponsContainer.GetBayStateWeapon(weaponIndex));

        if (weapon.MethodObtainingSkin == MethodObtainingSkin.Reward)
            Debug.Log($"Оружие {weaponIndex} успешно получено за рекламу");

        else if (weapon.MethodObtainingSkin == MethodObtainingSkin.InApp)
            Debug.Log($"Оружие {weaponIndex} успешно куплено за яны");

        SFXProvider.Play("SFX_UI_Button_Click_Select");
    }

    private void UpdateWalletDisplay()
    {
        if (walletBalanceText != null && Wallet.Instance != null)
        {
            int currentBalance = Wallet.Instance.Balance;
            walletBalanceText.text = currentBalance.ToString();
            Debug.Log($"Баланс в магазине обновлен: {currentBalance}");
        }
        else
        {
            if (walletBalanceText == null)
                Debug.LogWarning("walletBalanceText не назначен в инспекторе ShopUiManager!");

            if (Wallet.Instance == null)
                Debug.LogError("Wallet.Instance не найден!");
        }
    }

    private void OnBalanceChanged(int newBalance)
    {
        // Обновляем отображение баланса в магазине
        UpdateWalletDisplay();
        
        // Обновляем состояние кнопки покупки
        if (_currentWeapons != null)
        {
            // Находим индекс текущего оружия и обновляем кнопку
            for (int i = 0; i < _weaponsContainer.Weapons.Count; i++)
            {
                if (_weaponsContainer.Weapons[i] == _currentWeapons)
                {
                    getButton.UpdateButtonState(i);
                    break;
                }
            }
        }
    }

    private void BuyWeapon(MethodObtainingSkin type, int weaponIndex)
    {
        var weapon = _weaponsContainer.Weapons[weaponIndex];

        switch (type)
        {
            case MethodObtainingSkin.None:
                _weaponsContainer.TryBay(weaponIndex);
                _weaponSlot.SetWeapon(weaponIndex);
                ToolSaver.SaveCurrentTool(weaponIndex);
                break;
            case MethodObtainingSkin.Money:
                _weaponsContainer.TryBay(weaponIndex);
                _weaponSlot.SetWeapon(weaponIndex);
                ToolSaver.SaveCurrentTool(weaponIndex);
                break;
            case MethodObtainingSkin.Reward:
                YG2.RewardedAdvShow(weapon.PurchaseId, () => GiveWeapon(weapon.PurchaseId));
                break;
            case MethodObtainingSkin.InApp:
                YG2.BuyPayments(weapon.PurchaseId);
                break;
        }

        getButton.Enabled(weaponIndex, _weaponsContainer.GetBayStateWeapon(weaponIndex));
        getButton.transform.DOPunchScale(new Vector3(pressSize, pressSize, pressSize), pressDuration, 0, 0f);
        SFXProvider.Play("SFX_UI_Button_Click_Select");
    }

    private void SetWeapon(int weaponIndex)
    {
        var weapon = _weaponsContainer.Weapons[weaponIndex];
        
        if (_currentWeapons == weapon)
            return;
        
        if (_currentWeaponGameObject != null ) 
            Destroy(_currentWeaponGameObject);
        
        _currentWeaponGameObject = Instantiate(weapon.Prefab, weaponUiSlot);
        _currentWeapons = weapon;
        getButton.Enabled(weaponIndex, _weaponsContainer.GetBayStateWeapon(weaponIndex));
    }

    public void Open()
    {
        PauseHandler.Pause();
        CursorController.UnlockCursor();
        gameObject.SetActive(true);
        weaponCamera.gameObject.SetActive(true);

        SetWeapon(_weaponSlot.CurrentWeapons);
        UpdateWalletDisplay();
        
        characterThirdPersonController.SetPlayerState(ThirdPersonController.PlayerState.Busy);
    }

    public void Close(bool showInterstitialAdv)
    {
        gameObject.SetActive(false);
        weaponCamera.gameObject.SetActive(false);
        characterThirdPersonController.SetPlayerState(ThirdPersonController.PlayerState.Idle);

        if (showInterstitialAdv)
            YG2.InterstitialAdvShow();

        CursorController.LockCursor();
        PauseHandler.Play();
    }

    public void Close()
    {
        StartCoroutine(CloseWithAnimation());
    }

    private IEnumerator CloseWithAnimation()
    {
        closeButton.transform.DOPunchScale(new Vector3(pressSize, pressSize, pressSize), pressDuration, 0, 0f);
        SFXProvider.Play("SFX_UI_Button_Click_Select");
        yield return new WaitForSeconds(pressDuration / 2f);

        gameObject.SetActive(false);
        weaponCamera.gameObject.SetActive(false);
        characterThirdPersonController.SetPlayerState(ThirdPersonController.PlayerState.Idle);

        YG2.InterstitialAdvShow();
        CursorController.LockCursor();
        PauseHandler.Play();
    }

    private void ClearChild()
    {
        var children = weaponPanel.GetComponentsInChildren<WeaponSelectUI>();
        
        foreach (var child in children)
            Destroy(child.gameObject);
    }
    
    private void OnDestroy()
    {
        YG2.onPurchaseSuccess -= GiveWeapon;
        YG2.onRewardAdv -= GiveWeapon;

        if (Wallet.Instance != null)
            Wallet.Instance.BalanceChanged.RemoveListener(OnBalanceChanged);
    }
}