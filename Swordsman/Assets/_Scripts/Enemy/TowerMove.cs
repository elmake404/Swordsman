using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TowerMove : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent _agent;
    private Transform _target;


    [SerializeField]
    private float _speedMove;

    private bool _isActivation;
    [SerializeField]
    private float _activationZoneRadius;
    private float _sqrActivationZoneRadius;

    void Start()
    {
        _sqrActivationZoneRadius = (_activationZoneRadius * _activationZoneRadius);
        _target = PlayerMove.PlayerTransform;
        _agent.updatePosition = false;
        _agent.updateRotation = false;
    }

    void FixedUpdate()
    {
        if (_target != null)
        {
            if (_isActivation) MoveTower();                
            else if (!_isActivation && _sqrActivationZoneRadius >= (_target.position - transform.position).sqrMagnitude)
            {
                _isActivation = true;
            }

        }

    }

    private void MoveTower()
    {
        //transform.LookAt(_agent.steeringTarget);
        _agent.SetDestination(_target.position);
        transform.position = Vector3.MoveTowards(transform.position, _agent.steeringTarget, _speedMove);
        _agent.nextPosition = transform.position;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _activationZoneRadius);
    }
}
