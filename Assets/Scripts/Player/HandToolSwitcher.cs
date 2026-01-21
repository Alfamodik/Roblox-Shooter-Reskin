using UnityEngine;

public class HandToolSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject tool1;
    [SerializeField] private GameObject tool2;

    private void Start()
    {
        // В начале включаем только первый инструмент
        SetActiveTool(1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetActiveTool(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActiveTool(2);
        }
    }

    private void SetActiveTool(int toolNumber)
    {
        if (tool1 != null) tool1.SetActive(toolNumber == 1);
        if (tool2 != null) tool2.SetActive(toolNumber == 2);
    }
}
