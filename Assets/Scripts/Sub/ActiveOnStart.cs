using UnityEngine;

public class ActiveOnStart : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private bool _enabled;

    private void Awake()
    {
        if (_target != null)
            _target.SetActive(_enabled);

        else
            gameObject.SetActive(_enabled);
    }
}
