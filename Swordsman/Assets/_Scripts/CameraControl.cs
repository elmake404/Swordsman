using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    private Vector3 _offSet, _velocity = Vector3.zero;
    [SerializeField]
    private float _smoothTime;
    void Start()
    {
        _offSet = _target.position - transform.position;
    }

    void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, _target.position - _offSet, ref _velocity, _smoothTime*Time.deltaTime);
    }
}
