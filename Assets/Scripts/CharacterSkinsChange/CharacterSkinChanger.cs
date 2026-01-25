using Invector.vCharacterController;
using System;
using UnityEngine;

public class CharacterSkinChanger : MonoBehaviour
{
    public static event Action<vThirdPersonInput> CharacterChanged;
    
    [SerializeField] private vThirdPersonController _currentCharacter;

    public void Set(CharacterSkinItem characterSkinItem)
    {
        Vector3 position = _currentCharacter != null ? _currentCharacter.transform.position : transform.position;
        Quaternion rotation = _currentCharacter != null ? _currentCharacter.transform.rotation : transform.rotation;
        
        if (_currentCharacter != null)
            Destroy(_currentCharacter.gameObject);

        GameObject newCharacter = Instantiate(characterSkinItem.Prefab, position, rotation);
        _currentCharacter = newCharacter.GetComponent<vThirdPersonController>();
        
        vThirdPersonInput input = newCharacter.GetComponent<vThirdPersonInput>();
        input.unlockCursorOnStart = true;
        input.showCursorOnStart = true;
        
        CharacterChanged?.Invoke(input);
    }
}
