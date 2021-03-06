﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private Delimiter _delimiter;

    [SerializeField]
    private Shell _shell;
    [SerializeField]
    private Transform _shootPos;
    private Transform _target;

    [SerializeField]
    private float _speedRotation,_timeShoot, _activationZoneRadius;
    private float _sqrActivationZoneRadius , _timeShootСhanging;

    private void Awake()
    {
        CanvasManager.QuantityEnemy++;
    }

    private void Start()
    {
        _target = PlayerMove.PlayerTransform;
        _sqrActivationZoneRadius = (_activationZoneRadius * _activationZoneRadius);

        //StartCoroutine(Fire());
    }

    private void FixedUpdate()
    {
        if (_target!=null && CanvasManager.IsGameFlow)
        {
            if (_sqrActivationZoneRadius >= (_target.position - transform.position).sqrMagnitude)
            {
                if (RotationGan()&& _timeShootСhanging<=0)
                {
                    Instantiate(_shell, _shootPos.position, _shootPos.rotation);
                    _timeShootСhanging = _timeShoot;
                }
            }
        }

        if (_timeShootСhanging >= 0)
        {
            _timeShootСhanging -= Time.deltaTime;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Sword")
        {
            CanvasManager.Instance.AddProgress();

            _delimiter.Separation(collision.GetContact(0).point);
        }
    }
    private bool RotationGan()
    {
        Vector3 PosTarget = new Vector3(_target.position.x, transform.position.y, _target.position.z);
        Quaternion rotation = Quaternion.LookRotation(PosTarget - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation,rotation,_speedRotation);

        return (transform.rotation.eulerAngles - rotation.eulerAngles).magnitude <= 1.3f;
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, _activationZoneRadius);
    //}

}
