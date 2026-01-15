using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _unionSpawnPoints;
    [SerializeField] private List<Transform> _bossSpawnPoints;

    [Space(30f)]
    [SerializeField] private List<GameObject> _enemyPrefabs;
    [SerializeField] private List<GameObject> _bossPrefabs;
    [SerializeField] private Transform _enemyParent;

    [Space(30f)]
    [SerializeField] private float _spawnCooldown;
    [SerializeField, Range(0,1)] private float _bossSpawnRate;

    private int _instanceLimit;
    private EnemyList _enemyList;
    //private EnemyFactory _factory;

    public void Initialize(EnemyList enemyList, int instanceLimit)
    {
        _enemyList = enemyList;
        _instanceLimit = instanceLimit;
        //_factory = new EnemyFactory(_enemyPrefabs, _bossPrefabs);
    }

    public EnemyList EnemyList => _enemyList;

    public void StartWork()
    {
        StopWork();
        StartCoroutine(Spawn(_spawnCooldown));
    }

    public void StopWork() => StopAllCoroutines();

    private IEnumerator Spawn(float cooldown)
    {
        while(true)
        {
            for (int i = 0; i < _unionSpawnPoints.Count; i++)
            {
                if(_enemyList.Count >= _instanceLimit)
                {
                    yield return new WaitForSeconds(cooldown);
                    continue;
                }

                yield return new WaitForSeconds(cooldown);

                var spawnNumber = Random.Range(0f, 1f);

                GameObject instance;
                Vector3 instancePosinion;

                if(spawnNumber < _bossSpawnRate)
                {
                    instance = Instantiate(_bossPrefabs[Random.Range(0, _bossPrefabs.Count)]);
                    instancePosinion = _bossSpawnPoints[Random.Range(0, _bossSpawnPoints.Count)].position;
                }
                else
                {
                    instance = Instantiate(_enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)]);
                    instancePosinion = _unionSpawnPoints[i].position;
                }

                instance.transform.position = instancePosinion;
                instance.transform.parent = _enemyParent;

                if(instance.TryGetComponent(out IEnemy component))
                    _enemyList.Add(component);
                else
                    Debug.LogWarning($"Не удалось получить {nameof(IEnemy)}. Проверьте все элементы префабы на его наличие!");
            }
        }
    }
}
