using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponSelectUI : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent onSelect;

    [SerializeField] private Image iconImage;
    [SerializeField] private Color pressColor = Color.gray;

    [Space]
    [SerializeField] private float pointerEnterSize;
    [SerializeField] private float pointerEnterDuration;
    [SerializeField] private float pressSize;
    [SerializeField] private float pressDuration;

    private Vector3 _baseSize;
    private Color _baseColor;
    private Image _backImage;
    private bool _isSelect;

    private void Start()
    {
        _backImage = GetComponent<Image>();
        _baseColor = _backImage.color;
        _baseSize = gameObject.transform.localScale;
    }

    public void Initialize(Sprite icon)
    {
        iconImage.sprite = icon;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SFXProvider.Play("SFX_UI_Button_Click_Select");
        onSelect.Invoke();
        gameObject.transform.DOPunchScale(new Vector3(pressSize, pressSize, pressSize), pressDuration, 0, 0f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isSelect)
            return;

        _backImage.DOColor(pressColor, 0.2f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_isSelect)
            return;

        _backImage.DOColor(_baseColor, 0.2f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.transform.DOScale(new Vector3(pointerEnterSize, pointerEnterSize, pointerEnterSize), pressDuration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.transform.DOScale(_baseSize, pressDuration);
    }
}
