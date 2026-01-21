using UnityEngine;

public class ToolLoader : MonoBehaviour
{
    [SerializeField] private WeaponSlot weaponSlot;
    [SerializeField] private ToolsContainer weaponsContainer;
    
    // Добавляем отображение ID инструментов между сценами
    [SerializeField] private int[] toolIdMapping; // Индекс - ID в магазине, значение - ID в игре
    
    void Start()
    {
        // Загружаем сохраненный ID инструмента
        int shopToolId = ToolSaver.LoadCurrentTool();
        
        // Преобразуем ID из магазина в ID для текущей сцены
        int gameToolId = ConvertToolId(shopToolId);
        
        Debug.Log($"Загружен ID из магазина: {shopToolId}, преобразован в ID для игры: {gameToolId}");
        
        // Проверяем, есть ли WeaponSlot
        if (weaponSlot == null)
        {
            // Ищем WeaponSlot на сцене
            weaponSlot = FindObjectOfType<WeaponSlot>();
            
            // Если не нашли, создаем новый
            if (weaponSlot == null && weaponsContainer != null)
            {
                // Создаем GameObject для WeaponSlot
                GameObject weaponSlotObj = new GameObject("WeaponSlot");
                weaponSlot = weaponSlotObj.AddComponent<WeaponSlot>();
                
                // Устанавливаем WeaponsContainer через рефлексию
                var field = typeof(WeaponSlot).GetField("_weaponsContainer", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (field != null)
                {
                    field.SetValue(weaponSlot, weaponsContainer);
                }
            }
        }
        
        // Устанавливаем инструмент
        if (weaponSlot != null)
        {
            weaponSlot.SetWeapon(gameToolId);
            Debug.Log($"Установлен инструмент с ID: {gameToolId}");
        }
        else
        {
            Debug.LogError("WeaponSlot не найден и не может быть создан! Убедитесь, что WeaponsContainer назначен в ToolLoader.");
        }
    }
    
    // Метод для преобразования ID инструмента из магазина в ID для текущей сцены
    private int ConvertToolId(int shopToolId)
    {
        // Если отображение не задано или индекс вне диапазона, возвращаем исходный ID
        if (toolIdMapping == null || shopToolId >= toolIdMapping.Length || shopToolId < 0)
        {
            return shopToolId;
        }
        
        return toolIdMapping[shopToolId];
    }
}