using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private void Update()
    {
        gameObject.transform.LookAt(_target);
    }
}
