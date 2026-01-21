using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InputExample : MonoBehaviour
{
    [SerializeField] private NPCCharacter _npcCharacter;
    [SerializeField] private ResourcesHolder _experienceHolder;

    private Controller _characterController;
    private Controller _agentNPCContoller;
    private NavMeshPath _path;


    private void Awake()
    {
        _path = new NavMeshPath();


        //_agentNPCContoller = new AgentNPCController(_npcCharacter, _experienceHolder); // исправлено zxc
        //_agentNPCContoller.Enable(); zxc
    }


    private void Update()
    {

        _agentNPCContoller.Update(Time.deltaTime);
    }



}
