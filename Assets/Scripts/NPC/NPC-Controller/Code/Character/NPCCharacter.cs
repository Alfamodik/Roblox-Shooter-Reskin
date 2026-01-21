using UnityEngine;
using UnityEngine.AI;

public class NPCCharacter : MonoBehaviour
{
    private NavMeshAgent _agent;

    private NPCMover _mover;
    private DirectionalRotator _rotator;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Transform _target;

    public Vector3 CurrentVelocity => _agent.velocity;
    public Quaternion CurrentRotation => _rotator.CurrentRotation;
    [field: SerializeField] public float MiningTime { get; private set; }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;

        _mover = gameObject.AddComponent<NPCMover>();
        _mover.Init(_agent, _moveSpeed);
        _rotator = new DirectionalRotator(transform, _rotationSpeed);
    }

    private void Update() 
        => _rotator.Update(Time.deltaTime);

    private void OnDestroy()
        => _rotator.Dispose();

    public void SetDestination(Vector3 position) 
        => _mover.SetDestination(position);
    
    public void StopMove()
        => _mover.Stop();
    
    public void ResumeMove() 
        => _mover.Resume();
    
    public void SetRotationDirection(Vector3 inputDirection) 
        => _rotator.SetInputDirection(inputDirection);
    
    public void SetInstantRotation(Vector3 direction) 
        => _rotator.SetInstantRotation(direction);
    
    public bool TryGetPath(Vector3 targetPosition, NavMeshPath pathToTarget) 
        => NavMeshUtils.TryGetPath(_agent, targetPosition, pathToTarget);
}
