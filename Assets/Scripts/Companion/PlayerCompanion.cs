using Invector.vCharacterController.AI;
using UnityEngine;

[RequireComponent(typeof(vSimpleMeleeAI_Companion))]
public class PlayerCompanion : MonoBehaviour, IPauseble
{
    vSimpleMeleeAI_Companion vSimpleMeleeAICompanion;
    private bool _onPause;

    private void Awake()
    {
        vSimpleMeleeAICompanion = GetComponent<vSimpleMeleeAI_Companion>();
        //PauseHandler.Add(this); йцу
    }

    private void Start() => ReviveCompanion();

    private void Update()
    {
        if(_onPause)
            vSimpleMeleeAICompanion.lockMovement = true;
        else
            vSimpleMeleeAICompanion.lockMovement = false;
    }

    public void ReviveCompanion()
    {
        vSimpleMeleeAICompanion.isDead = false;
        vSimpleMeleeAICompanion.AddHealth(100);
        vSimpleMeleeAICompanion.Init();
    }

    public void SetPause(bool isPause) => _onPause = isPause;
}
