using Invector.vCharacterController;
using System;
using UnityEngine;

public class CharacterSkinChanger : MonoBehaviour
{
    public static event Action<vThirdPersonInput> CharacterChanged;
    
    [field: SerializeField] public vThirdPersonController CurrentCharacter { get; private set; }

    public void Set(CharacterSkinItem characterSkinItem)
    {
        Vector3 position = CurrentCharacter != null ? CurrentCharacter.transform.position : transform.position;
        Quaternion rotation = CurrentCharacter != null ? CurrentCharacter.transform.rotation : transform.rotation;
        
        if (CurrentCharacter != null)
            Destroy(CurrentCharacter.gameObject);

        GameObject newCharacter = Instantiate(characterSkinItem.Prefab, position, rotation);
        CurrentCharacter = newCharacter.GetComponent<vThirdPersonController>();
        
        vThirdPersonInput input = newCharacter.GetComponent<vThirdPersonInput>();
        input.unlockCursorOnStart = true;
        input.showCursorOnStart = true;
        
        CharacterChanged?.Invoke(input);
    }
}
