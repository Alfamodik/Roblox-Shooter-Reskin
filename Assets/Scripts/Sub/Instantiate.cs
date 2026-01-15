using UnityEngine;

public class Instantiate : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _transform;

    public void InstantiatePrefab() => Instantiate(_prefab, _transform);
}
