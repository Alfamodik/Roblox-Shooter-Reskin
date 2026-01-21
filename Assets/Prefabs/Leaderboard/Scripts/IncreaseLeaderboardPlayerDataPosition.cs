using System.Collections;
using TMPro;
using UnityEngine;

public class IncreaseLeaderboardPlayerDataPosition : MonoBehaviour
{
    [SerializeField] private float _offset;

    private void Start()
    {
        StartCoroutine(InitializePosition());
    }

    private IEnumerator InitializePosition()
    {
        yield return null;
        int position = int.Parse(transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text);
        transform.position = transform.position + _offset * position * Vector3.down;
    }
}
