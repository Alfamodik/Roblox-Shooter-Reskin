using UnityEngine;
using static YG.YG2;

public class HideObjectByDeviceType : MonoBehaviour
{
    [SerializeField] private Device _deviceType;

    void Start()
    {
        //if (envir.device == _deviceType) zxc
            gameObject.SetActive(false);
    }
}
