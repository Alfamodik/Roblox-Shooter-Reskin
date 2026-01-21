using UnityEngine;

public class WalletUIKeeper : MonoBehaviour
{
    private void Awake()
    {
        var objs = FindObjectsOfType<WalletUIKeeper>();
        
        if (objs.Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
    }
}