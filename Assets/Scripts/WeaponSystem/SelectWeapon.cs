using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectWeapon : MonoBehaviour
{
    public Action<SelectWeapon> OnSelected;

    [SerializeField] private Weapon _weapon;
    [SerializeField] private EquipWeapon _equipWeapon;

    [Space]
    [SerializeField] private Button _selectionButton;
    [SerializeField] private CanvasGroup _selectionFrame;

    public Weapon TargetWeapon => _weapon;

    private void Awake() => _selectionButton.onClick.AddListener(Select);

    private void OnDestroy() => _selectionButton.onClick.RemoveListener(Select);

    public void Deselect()
        => _selectionFrame.alpha = 0;

    public void Select()
    {
        WeaponDataProvider.TryGet(_weapon, out bool isUnlocked);

        if(isUnlocked == false)
            return;

        _selectionFrame.alpha = 1;
        OnSelected?.Invoke(this);
    }
}
