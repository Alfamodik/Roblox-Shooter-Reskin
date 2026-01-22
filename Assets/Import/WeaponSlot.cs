using System;
using System.Linq;
using UnityEngine;
using YG;

public class WeaponSlot : MonoBehaviour
{
    public int CurrentWeapons { get; private set; }

    [SerializeField] private ToolsContainer _toolsContainer;
    private GameObject _currentWeapon;
    
    private void Start()
    {
        Debug.Log($"объект: {gameObject.name}");
        Invoke(nameof(LoadSelectedWeapon), 0.1f);
    }
    
    public void Initialize(ToolsContainer toolsContainer)
    {
        _toolsContainer = toolsContainer;
    }

    public void SetWeapon(ToolSkinItem toolSkinItem) 
        => SetWeapon(_toolsContainer.Weapons.ToList().IndexOf(toolSkinItem));

    public void SetWeapon(int index)
    {
        if (_toolsContainer == null || index < 0 || index >= _toolsContainer.Weapons.Count)
        {
            Debug.LogError($"Некорректный индекс оружия: {index}, объект: {gameObject.name}");
            return;
        }
        
        if (_currentWeapon != null) 
            Destroy(_currentWeapon);

        var newWeapon = _toolsContainer.Weapons[index];
        
        if (newWeapon.Prefab != null)
        {
            _currentWeapon = Instantiate(newWeapon.Prefab, transform);
            CurrentWeapons = index;

            YG2.saves.СurrentToolId = index;
            YG2.SaveProgress();

            PlayerPrefs.SetInt("Selected_Weapon_ID", index);
            PlayerPrefs.Save();
            
            Debug.Log($"[WeaponSlot] Установлено и сохранено оружие: {newWeapon.Prefab.name} (ID: {index})");
        }
        else
        {
            Debug.LogError($"Префаб оружия с ID {index} равен null!");
        }
    }
    
    private void LoadSelectedWeapon()
    {
        Debug.Log($"[WeaponSlot] Загружаем выбранное оружие с ID: {YG2.saves.СurrentToolId}");
        SetWeapon(YG2.saves.СurrentToolId);
    }
}