using Invector;
using Invector.vCharacterController.AI;
using System;
using UnityEngine;

[RequireComponent(typeof(vObjectDamage))]
public class Zombe : MonoBehaviour, IEnemy, IPausable
{
    public event Action<IEnemy> IDie;

    [SerializeField] private bool _removeAfterDeatg;
    [SerializeField] private float _removabelTime;

    private vSimpleMeleeAI_Controller _controller;
    
    public bool OnPause { get; private set; }

    private void Awake()
    {
        _controller = GetComponent<vSimpleMeleeAI_Controller>();
        PauseHandler.Add(this);
    }

    private void Update()
    {
        if(OnPause)
            _controller.lockMovement = true;
        else
            _controller.lockMovement = false;
    }

    public void OnDie()
    {
        IDie?.Invoke(this);

        GetComponent<vObjectDamage>().damage.damageValue = 0;

        if(_removeAfterDeatg)
            Destroy(gameObject, _removabelTime);
    }

    public void Pause() => OnPause = true;

    public void Play() => OnPause = false;

    public void Kill() => Destroy(gameObject);
}
