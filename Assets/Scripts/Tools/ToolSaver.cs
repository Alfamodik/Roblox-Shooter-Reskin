using UnityEngine;
//using YG;

public static class ToolSaver
{
    public static void SaveCurrentTool(int toolId)
    {
        PlayerPrefs.SetInt("CurrentToolId", toolId);
        PlayerPrefs.Save();
        Debug.Log($"Сохранен инструмент с ID: {toolId}");
    }
    
    public static int LoadCurrentTool()
    {
        int toolId = PlayerPrefs.GetInt("CurrentToolId", 0);
        Debug.Log($"Загружен инструмент с ID: {toolId}");
        return toolId;
    }
}

/*namespace YG
{
    public partial class SavesYG
    {
        public int СurrentToolId;
    }
}*/
