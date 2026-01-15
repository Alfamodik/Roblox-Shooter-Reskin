using DG.Tweening;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class DoScaleTo : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Vector3 _endValue;
    [SerializeField] private float _duration;

    void Awake()
    {
        //StartCoroutine(Scaler());
        //AsyncScaler();
    }

    private async void AsyncScaler()
    {
        Debug.Log("First");
        DoTween.ChangeScale(_rectTransform, _endValue, _duration,
            Ease.Linear, UpdateType.Normal, true);

        await Task.Delay(0);

        Debug.Log("Second");
        DoTween.ChangeScale(_rectTransform, Vector3.one, _duration,
            Ease.Linear, UpdateType.Normal, true);

        await Task.Delay(1);

        Debug.Log("Third");
        DoTween.ChangeScale(_rectTransform, _endValue, _duration,
            Ease.Linear, UpdateType.Normal, true);

        await Task.Delay(10);
        Debug.Log("Four");
        DoTween.ChangeScale(_rectTransform, Vector3.one, _duration,
            Ease.Linear, UpdateType.Normal, true);

    }

    private IEnumerator Scaler()
    {
        yield return new WaitForSeconds(_duration);

        Debug.Log("First");
        DoTween.ChangeScale(_rectTransform, _endValue, _duration,
            Ease.Linear, UpdateType.Normal, true);

        yield return new WaitForSeconds(_duration);

        Debug.Log("Second");
        DoTween.ChangeScale(_rectTransform, Vector3.one, _duration,
            Ease.Linear, UpdateType.Normal, true);
    }
}
