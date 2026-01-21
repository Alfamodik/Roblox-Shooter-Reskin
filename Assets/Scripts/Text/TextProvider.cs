using System;
using TMPro;
using UnityEngine;

public class TextProvider : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentLevel;
    
    private string _startText;

    private void Start()
    {
        if (string.IsNullOrEmpty(_startText))
            Initialize();
    }

    public void SetEnding(string ending)
    {
        if (string.IsNullOrEmpty(_startText))
            Initialize();

        _currentLevel.text = _startText + ending;
    }

    private void Initialize()
    {
        _startText = _currentLevel.text;

        //if (TryGetComponent(out LanguageYG languageYG))
            //Destroy(languageYG);
    }
}