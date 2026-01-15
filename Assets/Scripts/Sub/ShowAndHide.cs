using UnityEngine;

public class ShowAndHide : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;

    public void Show() => _gameObject.SetActive(true);

    public void Hide() => _gameObject.SetActive(false);
}
