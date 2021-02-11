using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TowerMove : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent _agent;
    [SerializeField]
    private Rigidbody _rb;
    private Transform _target;


    [SerializeField]
    private float _speedMove, _speedRotation;

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
        if (_target != null && CanvasManager.IsGameFlow)
        {
            if (_isActivation) MoveTower();
            else if (!_isActivation && _sqrActivationZoneRadius >= (_target.position - transform.position).sqrMagnitude)
            {
                _isActivation = true;
            }

        }
        _rb.velocity = Vector3.zero;
    }

    private void MoveTower()
    {
        _agent.SetDestination(_target.position);
        RotationGan(_agent.steeringTarget);
        transform.position = Vector3.MoveTowards(transform.position, _agent.steeringTarget, _speedMove);
        _agent.nextPosition = transform.position;
    }
    private void RotationGan(Vector3 target)
    {
        Vector3 PosTarget = new Vector3(target.x, transform.position.y, target.z);
        Vector3 look = transform.position - PosTarget;
        if (look != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(look);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _speedRotation);
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, _activationZoneRadius);
    //}
}
