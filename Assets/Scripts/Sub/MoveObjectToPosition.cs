using UnityEngine;

public class MoveObjectToPosition : MonoBehaviour
{
    [SerializeField] private string _targetTag = "SoulTarget";
    [SerializeField] private float _speed;

    private Transform _targetTransform;

    private void Update()
    {
        _targetTransform = GameObject.FindWithTag(_targetTag).GetComponent<Transform>();
        transform.Translate((_targetTransform.position - transform.position).normalized * _speed);
    }
}
