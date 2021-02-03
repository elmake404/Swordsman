using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plyer : MonoBehaviour
{
    private Camera _cam;
    private Vector3 _startMousePos, _currenMousePos, _directionMove;
    [SerializeField]
    private float _speedRotationMax, _speedMoveMax, _rotationAcceleration;
    private float _speedRotation;
    void Start()
    {
        _speedRotation = _speedRotationMax;
        _cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startMousePos = _cam.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            if (_startMousePos == Vector3.zero)
            {
                _startMousePos = _cam.ScreenToViewportPoint(Input.mousePosition);
            }
            _currenMousePos = _cam.ScreenToViewportPoint(Input.mousePosition);
            if ((_currenMousePos - _startMousePos).sqrMagnitude>0.0001f)
            {
                Vector3 directionMose = (_currenMousePos - _startMousePos).normalized;

                _directionMove.x = directionMose.x;
                _directionMove.z = directionMose.y;
                transform.position += _directionMove * _speedMoveMax;
            }
            //Debug.Log((_currenMousePos - _startMousePos));
        }

    }
    private void FixedUpdate()
    {
        _speedRotation = Mathf.Lerp(_speedRotation,_speedRotationMax, _rotationAcceleration);
        transform.Rotate(Vector3.up * _speedRotation);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag =="Rock")
        {
            _speedRotation = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        var Enemy = other.GetComponent<Enemy>();

        if (Enemy != null)
        {
            Destroy(gameObject);
        }

    }
}
