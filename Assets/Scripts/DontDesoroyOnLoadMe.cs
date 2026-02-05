using UnityEngine;

public class DontDesoroyOnLoadMe : MonoBehaviour
{
    private void Awake() 
    {
        print("DontDesoroyOnLoadMe start Awake");
        DontDestroyOnLoad(gameObject);
        print("DontDesoroyOnLoadMe end Awake");
    }
}
