using UnityEngine;
using YG;

public class DeviceTypeBootstrap : MonoBehaviour
{
    [SerializeField] private GameObject _desktopUI;

    private void Start()
    {
        if(YG2.envir.isDesktop)
        {
            _desktopUI.SetActive(true);
        }
        else
        {
            _desktopUI.SetActive(false);
        }
    }
}
