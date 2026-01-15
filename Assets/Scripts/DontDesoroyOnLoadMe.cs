using UnityEngine;

public class DontDesoroyOnLoadMe : MonoBehaviour
{
    private void Awake() 
        => DontDestroyOnLoad(gameObject);
}
