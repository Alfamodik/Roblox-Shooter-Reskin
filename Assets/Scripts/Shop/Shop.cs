using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using YG;
using YG.Utils.Pay;

public class Shop : MonoBehaviour
{
    [Space]
    [SerializeField] private GameObject _shopCanvas;
    [SerializeField] private ShopContent _contentItems;

    [Space]
    [SerializeField] private ShopCategoryButton _characterSkinsButton;
    [SerializeField] private ShopCategoryButton _mazeSkinsButton;
    [SerializeField] private ShopCategoryButton _toolSkinsButton;
    [SerializeField] private ShopCategoryButton _petSkinsButton;
    [SerializeField] private ShopCategoryButton _weaponSkinsButton;

    [Space]
    [SerializeField] private Image _selectedText;
    [SerializeField] private Button _selectionButton;
    [SerializeField] private Button _rewardedButton;
    [SerializeField] private BuyButton _buyButton;
    [SerializeField] private BuyButton _inAppButton;
    [SerializeField] private ImageLoadYG _purchaseCurrencyImage;

    [SerializeField] private Button _closeButton;

    [Space]
    [SerializeField] private ShopPanel _shopPanel;
    [SerializeField] private SkinPlacement _skinPlacement;

    [Space]
    [SerializeField] private CharacterSkinChanger _characterSkinChanger;
    [SerializeField] private CursorController _cursorController;

    [Space]
    [SerializeField] private Camera _modelCamera;
    [SerializeField] private Transform _characterCategoryCameraPosition;
    [SerializeField] private Transform _mazeCategoryCameraPosition;
    [SerializeField] private Transform _toolCategoryCameraPosition;
    [SerializeField] private Transform _petCategoryCameraPosition;
    [SerializeField] private Transform _weaponCategoryCameraPosition;

    private WeaponSlot _weaponSlot;
    private SkinFinder _skinFinder;
    private IDataProvider _dataProvider;

    private ShopItemView _previewedItem;

    private SkinEquipper _skinEquipper;
    private SkinSelector _skinSelector;
    private SkinUnlocker _skinUnlocker;
    private OpenSkinsChecker _openSkinsChecker;
    private SelectedSkinChecker _selectedSkinChecker;

    public bool IsOpen { get; private set; }

    private void OnEnable()
    {
        _characterSkinsButton.Click += OnCharacterSkinsButtonClick;
        _mazeSkinsButton.Click += OnMazeSkinsButtonClick;
        _toolSkinsButton.Click += OnToolSkinsButtonClick;
        _petSkinsButton.Click += OnPetSkinsButtonClick;
        _weaponSkinsButton.Click += OnWeaponSkinsButtonClick;

        _shopPanel.ItemViewClicked += OnItemViewClicked;

        _buyButton.Click += OnBuyButtonClick;
        _inAppButton.Click += OnBuyButtonClick;
        _rewardedButton.onClick.AddListener(OnBuyButtonClick);
        _selectionButton.onClick.AddListener(OnSelectionButtonClick);
    }

    private void OnDisable()
    {
        _characterSkinsButton.Click -= OnCharacterSkinsButtonClick;
        _mazeSkinsButton.Click -= OnMazeSkinsButtonClick;
        _toolSkinsButton.Click -= OnToolSkinsButtonClick;
        _petSkinsButton.Click -= OnPetSkinsButtonClick;
        _weaponSkinsButton.Click -= OnWeaponSkinsButtonClick;

        _shopPanel.ItemViewClicked -= OnItemViewClicked;
        
        _buyButton.Click -= OnBuyButtonClick;
        _inAppButton.Click -= OnBuyButtonClick;
        _rewardedButton.onClick.RemoveListener(OnBuyButtonClick);
        _selectionButton.onClick.RemoveListener(OnSelectionButtonClick);
    }

    private void OnDestroy()
    {
        YG2.onRewardAdv -= GiveSkinByCode;
        YG2.onPurchaseSuccess -= GiveSkinByCode;
    }

    public void Initialize(IDataProvider dataProvider, OpenSkinsChecker openSkinsChecker, SelectedSkinChecker selectedSkinChecker, SkinSelector skinSelector, SkinUnlocker skinUnlocker)
    {
        _selectedSkinChecker = selectedSkinChecker;
        _openSkinsChecker = openSkinsChecker;   
        _skinSelector = skinSelector;
        _skinUnlocker = skinUnlocker;

        _skinEquipper = new SkinEquipper(_characterSkinChanger, _weaponSlot);
        _skinFinder = new SkinFinder(_contentItems);
        
        _dataProvider = dataProvider;

        _shopPanel.Initialize(openSkinsChecker, selectedSkinChecker);

        YG2.onRewardAdv += GiveSkinByCode;
        YG2.onPurchaseSuccess += GiveSkinByCode;

        _shopPanel.ItemViewClicked += OnItemViewClicked;
        _closeButton.onClick.AddListener(Close);

        //_characterSkinChanger.WeaponSlotChanged += UpdateWeaponSlot;
    }

    private void UpdateWeaponSlot(WeaponSlot weaponSlot)
    {
        _weaponSlot = weaponSlot;
        _skinEquipper.Reinitialize(_weaponSlot);
    }

    public void Open()
    {
        IsOpen = true;

        _shopCanvas.SetActive(true);
        PauseHandler.Pause();
        
        if (_cursorController != null)
            _cursorController.UnlockCursor();

        OnCharacterSkinsButtonClick();
    }

    public void Close()
    {
        IsOpen = false;
        
        _shopCanvas.SetActive(false);
        PauseHandler.Play();
        
        if (_cursorController != null)
            _cursorController.LockCursor();

        if (YG2.isTimerAdvCompleted)
            YG2.InterstitialAdvShow();
    }

    private void OnItemViewClicked(ShopItemView item)
    {
        _previewedItem = item;
        _skinPlacement.InstantiateModel(_previewedItem.Model);

        _openSkinsChecker.Visit(_previewedItem.Item);

        if (_openSkinsChecker.IsOpened)
        {
            _selectedSkinChecker.Visit(_previewedItem.Item);

            if (_selectedSkinChecker.IsSelected)
            {
                ShowSelectedText();
                return;
            }

            ShowSelectionButton();
        }
        else
        {
            ShowBuyButton(_previewedItem.Price);
        }
    }

    private void OnBuyButtonClick()
    {
        void Unlock()
        {
            _skinUnlocker.Visit(_previewedItem.Item);
            SelectSkin();
            _previewedItem.Unlock();
            _dataProvider.Save();
        }

        switch (_previewedItem.Item.MethodObtainingSkin)
        {
            case MethodObtainingSkin.None:
                Unlock();
                break;

            case MethodObtainingSkin.Money:
                if (Wallet.Instance.IsEnough(_previewedItem.Price))
                {
                    Wallet.Instance.Spend(_previewedItem.Price);
                    Unlock();
                }

                break;

            case MethodObtainingSkin.Reward:
                YG2.RewardedAdvShow(_previewedItem.Item.PurchaseId);
                break;

            case MethodObtainingSkin.InApp:
                YG2.BuyPayments(_previewedItem.Item.PurchaseId);
                break;

            default:
                throw new InvalidImplementationException();
        }
    }

    private void GiveSkinByCode(string purchaseId)
    {
        ShopItem shopItem = _skinFinder.Find(purchaseId);
        _skinUnlocker.Visit(shopItem);

        SelectSkin();
        _previewedItem.Unlock();
        _dataProvider.Save();
    }

    private void OnSelectionButtonClick()
    {
        SelectSkin();

        _dataProvider.Save();
    }

    private void OnCharacterSkinsButtonClick()
    {
        _characterSkinsButton.Select();
        _mazeSkinsButton.Unselect();
        _toolSkinsButton.Unselect();
        _weaponSkinsButton.Unselect();
        _petSkinsButton.Unselect();

        UpdateCameraTransform(_characterCategoryCameraPosition);

        _shopPanel.Show(_contentItems.CharacterSkinItems.Cast<ShopItem>());
    }

    private void OnMazeSkinsButtonClick()
    {
        _characterSkinsButton.Unselect();
        _mazeSkinsButton.Select();
        _toolSkinsButton.Unselect();
        _weaponSkinsButton.Unselect();
        _petSkinsButton.Unselect();

        UpdateCameraTransform(_mazeCategoryCameraPosition);

        _shopPanel.Show(_contentItems.MazeSkinItems.Cast<ShopItem>());
    }

    private void OnToolSkinsButtonClick()
    {
        _characterSkinsButton.Unselect();
        _mazeSkinsButton.Unselect();
        _toolSkinsButton.Select();
        _weaponSkinsButton.Unselect();
        _petSkinsButton.Unselect();

        UpdateCameraTransform(_toolCategoryCameraPosition);

        _shopPanel.Show(_contentItems.ToolSkinItems.Cast<ShopItem>());
    }

    private void OnWeaponSkinsButtonClick()
    {
        _characterSkinsButton.Unselect();
        _mazeSkinsButton.Unselect();
        _toolSkinsButton.Unselect();
        _weaponSkinsButton.Select();
        _petSkinsButton.Unselect();

        UpdateCameraTransform(_weaponCategoryCameraPosition);

        _shopPanel.Show(_contentItems.WeaponSkinItems.Cast<ShopItem>());
    }

    private void OnPetSkinsButtonClick()
    {
        _characterSkinsButton.Unselect();
        _mazeSkinsButton.Unselect();
        _toolSkinsButton.Unselect();
        _weaponSkinsButton.Unselect();
        _petSkinsButton.Select();

        UpdateCameraTransform(_petCategoryCameraPosition);

        _shopPanel.Show(_contentItems.PetSkinItems.Cast<ShopItem>());
    }

    private void UpdateCameraTransform(Transform transform)
    {
        _modelCamera.transform.position = transform.position;
        _modelCamera.transform.rotation = transform.rotation;
    }

    private void SelectSkin()
    {
        _skinSelector.Visit(_previewedItem.Item);
        _skinEquipper.Visit(_previewedItem.Item);
        
        _shopPanel.Select(_previewedItem);
        ShowSelectedText();
    }

    private void ShowSelectionButton()
    {
        HideInteractButton();
        _selectionButton.gameObject.SetActive(true);
    }

    private void ShowSelectedText()
    {
        HideInteractButton();
        _selectedText.gameObject.SetActive(true);
    }

    private void ShowRewardedButton()
    {
        HideInteractButton();
        _rewardedButton.gameObject.SetActive(true);
    }

    private void ShowInAppButton()
    {
        HideInteractButton();
        _inAppButton.gameObject.SetActive(true);
    }

    private void ShowBuyButton(int price)
    {
        HideInteractButton();

        void ActivateBuyButton()
        {
            _buyButton.gameObject.SetActive(true);
            _buyButton.UpdateText(price);
        }

        switch (_previewedItem.Item.MethodObtainingSkin)
        {
            case MethodObtainingSkin.None:
                ActivateBuyButton();
                break;

            case MethodObtainingSkin.Money:
                ActivateBuyButton();

                if (Wallet.Instance.IsEnough(price))
                    _buyButton.Unlock();
                else
                    _buyButton.Lock();

                break;

            case MethodObtainingSkin.Reward:
                ShowRewardedButton();
                break;

            case MethodObtainingSkin.InApp:
                ShowInAppButton();
                _inAppButton.UpdateText(price);

                Purchase purchase = YG2.PurchaseByID(_previewedItem.Item.PurchaseId);
                _purchaseCurrencyImage.urlImage = purchase.currencyImageURL;
                _purchaseCurrencyImage.Load();

                break;

            default:
                throw new InvalidImplementationException();
        }
    }

    private void HideInteractButton()
    {
        HideBuyButton();
        HideSelectionButton();
        HideSelectedText();
        HideRewardedButton();
        HideInAppButton();
    }

    private void HideBuyButton() => _buyButton.gameObject.SetActive(false);
    private void HideSelectionButton() => _selectionButton.gameObject.SetActive(false);
    private void HideSelectedText() => _selectedText.gameObject.SetActive(false);
    private void HideRewardedButton() => _rewardedButton.gameObject.SetActive(false);
    private void HideInAppButton() => _inAppButton.gameObject.SetActive(false);
}
