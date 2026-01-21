using Invector;
using Invector.vCharacterController.AI;
using System;
using UnityEngine;

[RequireComponent(typeof(vObjectDamage))]
public class Zombe : MonoBehaviour, IEnemy, IPauseble
{
    public event Action<IEnemy> IDie;

    [SerializeField] private bool _removeAfterDeatg;
    [SerializeField] private float _removabelTime;

    private vSimpleMeleeAI_Controller _controller;
    private bool _onPause;

    private void Awake()
    {
        _controller = GetComponent<vSimpleMeleeAI_Controller>();
        //PauseHandler.Add(this); zxc
    }

    private void Update()
    {
        if(_onPause)
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

    public void SetPause(bool isPause) => _onPause = isPause;

    public void Kill() => Destroy(gameObject);
}
