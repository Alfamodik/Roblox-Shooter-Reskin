using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using YG;

[RequireComponent(typeof(Image))]
public class ShopItemView : MonoBehaviour, IPointerClickHandler
{
    public event Action<ShopItemView> Click;

    [SerializeField] private Sprite _standardBackground;
    [SerializeField] private Sprite _highlightBackground;

    [SerializeField] private Image _contentImage;
    [SerializeField] private Image _lockImage;

    [SerializeField] private IntValueView _moneyPriceView;
    [SerializeField] private GameObject _rewardedPriceView;
    [SerializeField] private IntValueView _currencyPriceView;
    //[SerializeField] private ImageLoadYG _purchaseCurrencyImage;

    [SerializeField] private Image _selectionText;

    private Image _backgroundImage;

    public ShopItem Item { get; private set; }

    public bool IsLock { get; private set; }

    public int Price => Item.Price;
    public GameObject Model => Item.PrewiewPrefab;

    public void Initialize(ShopItem item)
    {
        _backgroundImage = GetComponent<Image>();
        _backgroundImage.sprite = _standardBackground;

        Item = item;
        _contentImage.sprite = item.Image;

        ShowPrice();
    }

    public void OnPointerClick(PointerEventData eventData) => Click?.Invoke(this);

    public void Lock()
    {
        IsLock = true;
        _lockImage.gameObject.SetActive(IsLock);
        ShowPrice();
    }

    public void Unlock()
    {
        IsLock = false;
        _lockImage.gameObject.SetActive(IsLock);
        HidePrice();
    }

    public void Select()
    {
        _selectionText.gameObject.SetActive(true);
    }

    public void Unselect()
    {
        _selectionText.gameObject.SetActive(false);
    }

    public void Highlight() => _backgroundImage.sprite = _highlightBackground;
    public void UnHighlight() => _backgroundImage.sprite = _standardBackground;

    public void LoadCurrency()
    {
        /*if (Item.MethodObtainingSkin != MethodObtainingSkin.InApp || !string.IsNullOrWhiteSpace(_purchaseCurrencyImage.urlImage))
            return;

        Purchase purchase = YG2.PurchaseByID(Item.PurchaseId);
        _purchaseCurrencyImage.urlImage = purchase.currencyImageURL;
        _purchaseCurrencyImage.Load();*/
    }

    private void HidePrice()
    {
        _moneyPriceView.Hide();
        _rewardedPriceView.SetActive(false);
        _currencyPriceView.Hide();
    }

    private void ShowPrice()
    {
        HidePrice();

        switch (Item.MethodObtainingSkin)
        {
            case MethodObtainingSkin.None:
                _moneyPriceView.Show(Price);
                break;

            case MethodObtainingSkin.Money:
                _moneyPriceView.Show(Price);
                break;

            case MethodObtainingSkin.Reward:
                _rewardedPriceView.SetActive(true);
                break;

            case MethodObtainingSkin.InApp:
                _currencyPriceView.Show(Price);
                break;

            default:
                throw new NotImplementedException();
        }
    }
}
