using UnityEngine;

public class ShopInputController : MonoBehaviour
{
    [SerializeField] private Shop _shop;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_shop.IsOpen)
                _shop.Close();
            else
                _shop.Open();
        }
    }
}
