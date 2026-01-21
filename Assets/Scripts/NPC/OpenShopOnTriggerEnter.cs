using UnityEngine;

public class OpenShopOnTriggerEnter : MonoBehaviour
{
    [SerializeField] private Shop _shop;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            _shop.Open();
    }
}