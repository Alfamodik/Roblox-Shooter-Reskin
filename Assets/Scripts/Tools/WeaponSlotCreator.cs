using UnityEngine;

public class WeaponSlotCreator : MonoBehaviour
{
    [SerializeField] private ToolsContainer weaponsContainer;
    [SerializeField] private Transform weaponSpawnPoint; // Существующая точка спауна оружия
    
    // Добавляем отображение ID инструментов между сценами
    [SerializeField] private int[] toolIdMapping; // Индекс - ID в магазине, значение - ID в игре
    [SerializeField] private bool debugMode = true; // Режим отладки
    
    void Start()
    {
        if (weaponsContainer == null)
        {
            Debug.LogError("WeaponsContainer не назначен в WeaponSlotCreator!");
            return;
        }
        
        // Загружаем сохраненный ID инструмента
        int shopToolId = ToolSaver.LoadCurrentTool();
        
        // Преобразуем ID из магазина в ID для текущей сцены
        int gameToolId = ConvertToolId(shopToolId);
        
        if (debugMode)
        {
            Debug.Log($"Загружен ID из магазина: {shopToolId}, преобразован в ID для игры: {gameToolId}");
        }
        
        // Проверяем, существует ли уже WeaponSlot
        WeaponSlot existingSlot = FindObjectOfType<WeaponSlot>();
        if (existingSlot != null)
        {
            // Используем существующий слот
            SetupWeaponSlot(existingSlot, gameToolId);
            return;
        }
        
        // Создаем GameObject для WeaponSlot
        GameObject weaponSlotObj;
        
        if (weaponSpawnPoint != null)
        {
            // Используем существующую точку спауна
            weaponSlotObj = weaponSpawnPoint.gameObject;
        }
        else
        {
            // Создаем новый объект
            weaponSlotObj = new GameObject("WeaponSlot");
        }
        
        // Добавляем компонент WeaponSlot
        WeaponSlot weaponSlot = weaponSlotObj.AddComponent<WeaponSlot>();
        
        // Устанавливаем WeaponsContainer через рефлексию
        var field = typeof(WeaponSlot).GetField("_weaponsContainer", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (field != null)
        {
            field.SetValue(weaponSlot, weaponsContainer);
        }
        
        // Устанавливаем оружие
        SetupWeaponSlot(weaponSlot, gameToolId);
    }
    
    private void SetupWeaponSlot(WeaponSlot weaponSlot, int toolId)
    {
        // Проверяем, что ID в допустимом диапазоне
        if (toolId >= 0 && toolId < weaponsContainer.Weapons.Count)
        {
            weaponSlot.SetWeapon(toolId);
            
            if (debugMode)
            {
                Debug.Log($"Установлен инструмент с ID: {toolId}");
            }
        }
        else
        {
            Debug.LogError($"Недопустимый ID инструмента: {toolId}. Допустимый диапазон: 0-{weaponsContainer.Weapons.Count - 1}");
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