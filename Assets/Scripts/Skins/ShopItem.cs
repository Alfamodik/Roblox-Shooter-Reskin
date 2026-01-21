using UnityEngine;

public abstract class ShopItem : ScriptableObject
{
    [field: SerializeField] public GameObject PrewiewPrefab { get; private set; }
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public int SerialNumber { get; private set; }
    [field: SerializeField] public string PurchaseId { get; private set; }
    [field: SerializeField] public MethodObtainingSkin MethodObtainingSkin { get; private set; }
}
