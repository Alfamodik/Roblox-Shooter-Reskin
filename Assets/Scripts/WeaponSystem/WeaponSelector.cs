using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelector : MonoBehaviour
{
    [SerializeField] private EquipWeapon _equipWeapon;
    [SerializeField] private Button _equipButton;

    [Space]
    [SerializeField] private List<SelectWeapon> _selectWeaponList = new();

    private Weapon _selectedWeapon = Weapon.Handgun;

    private void Awake()
    {
        _equipButton.onClick.AddListener(EquipSelectedWeapon);

        foreach(var item in _selectWeaponList)
            item.OnSelected += DeselectOther;

        _selectWeaponList[0].Select();
    }

    private void EquipSelectedWeapon()
    {
        _equipWeapon.Equip(_selectedWeapon);
    }

    private void OnDestroy()
    {
        _equipButton.onClick.RemoveListener(EquipSelectedWeapon);

        foreach(var item in _selectWeaponList)
            item.OnSelected -= DeselectOther;
    }

    private void DeselectOther(SelectWeapon weapon)
    {
        _selectedWeapon = weapon.TargetWeapon;

        foreach(var item in _selectWeaponList)
            if(item != weapon)
                item.Deselect();
    }
}
