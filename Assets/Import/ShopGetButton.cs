using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YG;
using YG.LanguageLegacy;
using YG.Utils.Pay;

public class ShopGetButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image iconImageMoney;
    [SerializeField] private Image iconImageMovie;
    [SerializeField] private Image iconImageYan;

    [SerializeField] private ToolsContainer _weaponsContainer;
    [SerializeField] private WeaponSlot _weaponSlot;

    [SerializeField] private TMP_Text _get;
    [SerializeField] private TMP_Text _put;
    [SerializeField] private TMP_Text _putOn;

    private string _getStartText;

    [HideInInspector] public UnityEvent<MethodObtainingSkin,int> tryGet;

    private void Awake()
    {
        _get.gameObject.SetActive(true);
        _put.gameObject.SetActive(true);
        _putOn.gameObject.SetActive(true);
    }

    private void Start()
    {
        _getStartText = _get.text;

        Destroy(_get.GetComponent<LanguageYG>());
        Destroy(_put.GetComponent<LanguageYG>());
        Destroy(_putOn.GetComponent<LanguageYG>());

        _get.gameObject.SetActive(false);
        _put.gameObject.SetActive(false);
        _putOn.gameObject.SetActive(true);
    }

    public void Reset()
    {
        button.interactable = true;
        _get.color = Color.white;
        _put.color = Color.white;
        _putOn.color = Color.white;
        button.onClick.RemoveAllListeners();
        gameObject.SetActive(false);
    }

    public void Enabled(int weaponIndex, bool isPurchased)
    {
        Reset();
        gameObject.SetActive(true);

        var weapon = _weaponsContainer.Weapons[weaponIndex];
        
        if (_weaponSlot.CurrentWeapons == weaponIndex)
        {
            _get.gameObject.SetActive(false);
            _put.gameObject.SetActive(false);
            _putOn.gameObject.SetActive(true);

            iconImageMoney.gameObject.SetActive(false);
            iconImageMovie.gameObject.SetActive(false);
            iconImageYan.gameObject.SetActive(false);

            button.interactable = false;
        }
        else
        {
            if (isPurchased)
            {
                _get.gameObject.SetActive(false);
                _put.gameObject.SetActive(true);
                _putOn.gameObject.SetActive(false);

                iconImageMoney.gameObject.SetActive(false);
                iconImageMovie.gameObject.SetActive(false);
                iconImageYan.gameObject.SetActive(false);

                button.onClick.AddListener(() => tryGet.Invoke(MethodObtainingSkin.None, weaponIndex));
            }
            else
            {
                _get.gameObject.SetActive(true);
                _put.gameObject.SetActive(false);
                _putOn.gameObject.SetActive(false);

                switch (weapon.MethodObtainingSkin)
                {
                    case MethodObtainingSkin.Money:
                        _get.SetText($"{_getStartText} {weapon.Price}");

                        iconImageMoney.gameObject.SetActive(true);
                        iconImageMovie.gameObject.SetActive(false);
                        iconImageYan.gameObject.SetActive(false);

                        button.onClick.AddListener(() => tryGet.Invoke(MethodObtainingSkin.Money, weaponIndex));
                        CheckCanAfford(weapon.Price);
                        break;

                    case MethodObtainingSkin.Reward:
                        _get.SetText(_getStartText);

                        iconImageMoney.gameObject.SetActive(false);
                        iconImageMovie.gameObject.SetActive(true);
                        iconImageYan.gameObject.SetActive(false);

                        button.onClick.AddListener(() => tryGet.Invoke(MethodObtainingSkin.Reward, weaponIndex));
                        break;

                    case MethodObtainingSkin.InApp:
                        Purchase purchase = YG2.PurchaseByID(weapon.PurchaseId);
                        _get.SetText($"{_getStartText} {purchase.price}");

                        iconImageMoney.gameObject.SetActive(false);
                        iconImageMovie.gameObject.SetActive(false);
                        iconImageYan.gameObject.SetActive(true);

                        ImageLoadYG imageLoadYG = GetComponent<ImageLoadYG>();

                        imageLoadYG.urlImage = purchase.currencyImageURL;
                        imageLoadYG.Load();

                        button.onClick.AddListener(() => tryGet.Invoke(MethodObtainingSkin.InApp, weaponIndex));
                        break;
                }
            }
        }
    }

    public void UpdateButtonState(int weaponIndex)
    {
        var weapon = _weaponsContainer.Weapons[weaponIndex];

        if (weapon.MethodObtainingSkin == MethodObtainingSkin.Money)
        {
            CheckCanAfford(weapon.Price);
        }
    }

    private void CheckCanAfford(int cost)
    {
        if (Wallet.Instance == null)
        {
            Debug.LogError("Wallet.Instance не найден!");
            button.interactable = false;
            return;
        }
        
        int currentBalance = Wallet.Instance.Balance;
        
        if (currentBalance >= cost)
        {
            button.interactable = true;
            Debug.Log($"Покупка доступна. Баланс: {currentBalance}, стоимость: {cost}");
        }
        else
        {
            button.interactable = false;
            Debug.Log($"Недостаточно денег. Баланс: {currentBalance}, нужно: {cost}");

            _get.color = Color.red;
            _put.color = Color.red;
            _putOn.color = Color.red;
        }
    }
}