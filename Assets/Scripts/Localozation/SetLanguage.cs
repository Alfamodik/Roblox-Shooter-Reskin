using GamePush;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class SetLanguage : MonoBehaviour
{
    [SerializeField] private bool _updateOnAwake = true;

    [Space(30f)]
    [SerializeField] private string _english;
    [SerializeField] private string _turkish;
    
    private string _french;

    private void Awake()
    {
        if (_updateOnAwake)
            Initialize();
    }

    public void Initialize()
    {
        switch(GP_Language.Current())
        {
            case Language.Russian:
                return;

            case Language.Turkish:
                GetComponent<TMP_Text>().text = _turkish;
                return;
        }

        GetComponent<TMP_Text>().text = _english;
    }
}
