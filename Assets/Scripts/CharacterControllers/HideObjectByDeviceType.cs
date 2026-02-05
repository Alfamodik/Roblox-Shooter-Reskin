using UnityEngine;
using static YG.YG2;

public class HideObjectByDeviceType : MonoBehaviour
{
    [SerializeField] private Device _deviceType;

    void Start()
    {
        print("HideObjectByDeviceType start Start");
        /*if (envir.device == _deviceType)
            gameObject.SetActive(false);*/
        print("HideObjectByDeviceType end Start");
    }
}
