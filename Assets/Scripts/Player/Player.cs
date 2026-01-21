
using UnityEngine;

public class Player : MonoBehaviour, ITeleportable
{
    public void Teleport(Vector3 targetPosition)
    {
        CharacterController characterController = GetComponent<CharacterController>();
        if (characterController != null)
        {
            characterController.enabled = false;
            transform.position = targetPosition;
            transform.rotation = Quaternion.identity; // Сброс вращения
            characterController.enabled = true;
        }
        else
        {
            transform.position = targetPosition;
            transform.rotation = Quaternion.identity; // Сброс вращения
        }
    }
}