using UnityEngine;
using UnityEngine.AI;

public class NPCMover : MonoBehaviour, IPausable
{
    private NavMeshAgent _agent;
    private bool _isStopped;

    public bool OnPause { get; private set; }
    public Vector3 CurrentlyVelocity => _agent.desiredVelocity;

    private void OnDestroy()
        => PauseHandler.Remove(this);

    public void Init(NavMeshAgent agent, float movementSpeed)
    {
        _agent = agent;
        _agent.speed = movementSpeed;
        _agent.acceleration = 999;

        PauseHandler.Add(this);
    }

    public void SetDestination(Vector3 position)
    {
        _agent.SetDestination(position);
    }

    public void Stop()
    {
        _isStopped = true;
        
        if (!OnPause)
            _agent.isStopped = true;
    }

    public void Resume()
    {
        _isStopped = false;
        
        if (!OnPause)
            _agent.isStopped = false;
    }

    public void Pause()
    {
        OnPause = true;
        _agent.isStopped = true;
    }

    public void Play()
    {
        OnPause = false;
        _agent.isStopped = _isStopped;
    }
}
