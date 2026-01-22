using UnityEngine;
using YG;

public static class ToolSaver
{
    public static void SaveCurrentTool(int toolId)
    {
        YG2.saves.СurrentToolId = toolId;
        YG2.SaveProgress();
        Debug.Log($"Сохранен инструмент с ID: {toolId}");
    }
    
    public static int LoadCurrentTool()
    {
        Debug.Log($"Загружен инструмент с ID: {YG2.saves.СurrentToolId}");
        return YG2.saves.СurrentToolId;
    }
}

namespace YG
{
    public partial class SavesYG
    {
        public int СurrentToolId;
    }
}
