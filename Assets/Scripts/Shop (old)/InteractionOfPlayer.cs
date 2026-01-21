using UnityEngine;

public class InteractionOfPlayer : MonoBehaviour
{
    // Метод для попытки взаимодействия с ресурсом с помощью инструмента
    public void TryUseToolOnResource(GameObject resourceObject, GameObject toolObject)
    {
        IResource resource = resourceObject.GetComponent<IResource>();
        ITool tool = toolObject.GetComponent<ITool>();
        if (resource != null && tool != null)
        {
            if (resource.Interact(tool))
            {
                Debug.Log("Инструмент подходит! Можно взаимодействовать.");
                // Здесь логика сбора ресурса или другое действие
            }
            else
            {
                Debug.Log("Инструмент не подходит для этого ресурса.");
            }
        }
        else
        {
            Debug.Log("Ресурс или инструмент не найден на объектах.");
        }
    }
}
