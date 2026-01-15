using GamePush;
using UnityEngine;

public class GameStartBootstrap : MonoBehaviour
{
    private void Start()
    {
        GP_Game.GameplayStart();
        GP_Game.GameReady();
    }
}
