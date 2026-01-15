using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyFactory
{
    private readonly List<GameObject> _unitPrefabs;
    private readonly List<GameObject> _bossPrefabs;

    public EnemyFactory(List<GameObject> unitPrefabs, List<GameObject> bossPrefabs)
    {
        _unitPrefabs = unitPrefabs;
        _bossPrefabs = bossPrefabs;
    }

    public GameObject GetUnit() => Object.Instantiate(GetUnitRandomPrefab());

    public GameObject GetBoss() => Object.Instantiate(GetBossRandomPrefab());

    private GameObject GetUnitRandomPrefab() => _unitPrefabs[Random.Range(0, _unitPrefabs.Count)];

    private GameObject GetBossRandomPrefab() => _bossPrefabs[Random.Range(0, _bossPrefabs.Count)];
}
