using DG.Tweening;
using System.Globalization;
using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TMP_Text _value;
    [SerializeField] private string _numberGroupSeparator;

    [Space]
    [SerializeField] private RectTransform _saleObject;
    [SerializeField] private Vector3 _punshScale;
    [SerializeField] private float _animationDuration;

    private Tweener _tweener;

    public void Start()
    {
        UpdateValue(Wallet.Instance.Balance);
        Wallet.Instance.BalanceChanged.AddListener(UpdateValue);
    }

    private void OnDestroy() => Wallet.Instance?.BalanceChanged?.RemoveListener(UpdateValue);

    private void UpdateValue(int value)
    {
        CultureInfo cultureInfo = new("ru-RU");
        cultureInfo.NumberFormat.NumberGroupSeparator = _numberGroupSeparator;
        _value.text = value.ToString("N0", cultureInfo);

        _tweener?.Kill(true);
        _tweener = _saleObject
            .DOPunchScale(_punshScale, _animationDuration)
            .SetRelative();
    }
}
