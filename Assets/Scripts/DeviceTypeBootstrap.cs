using UnityEngine;
using YG;

public class DeviceTypeBootstrap : MonoBehaviour
{
    [SerializeField] private GameObject _desktopUI;

    private void Start()
    {
        print("DeviceTypeBootstrap start Start");
        if(YG2.envir.isDesktop)
        {
            _desktopUI.SetActive(true);
        }
        else
        {
            _desktopUI.SetActive(false);
        }
        print("DeviceTypeBootstrap end Start");
    }
}
