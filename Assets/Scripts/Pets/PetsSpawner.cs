using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PetsSpawner : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private Transform _centralPoint;
    [SerializeField] private float _minOffset;
    [SerializeField] private float _maxOffset;

    [Space]
    [SerializeField] private ShopContent _contentItems;

    private IPersistentData _iPersistentData;
    private readonly List<PetSkinItem> _spawnedPets = new();

    public void Initialize(IPersistentData iPersistentData)
    {
        _iPersistentData = iPersistentData;
        SpawnPets();
    }

    public void SpawnPet(PetSkinItem petSkinItem)
    {
        if (_spawnedPets.Contains(petSkinItem))
            return;

        _spawnedPets.Add(petSkinItem);

        Vector3 spawnPoint = _centralPoint.position;

        float xOffset = Random.Range(_minOffset, _maxOffset);
        float zOffset = Random.Range(_minOffset, _maxOffset);

        spawnPoint.x += Random.Range(0, 1) == 0 ? xOffset : -xOffset;
        spawnPoint.z += Random.Range(0, 1) == 0 ? zOffset : -zOffset;

        Instantiate(petSkinItem.Prefab, spawnPoint, Quaternion.identity, _parent);
    }

    private void SpawnPets()
    {
        foreach (PetSkins item in _iPersistentData.PlayerData.OpenPetSkins)
        {
            PetSkinItem petSkinItem = _contentItems.PetSkinItems.First(pet => pet.SkinType == item);    
            SpawnPet(petSkinItem);
        }
    }
}
