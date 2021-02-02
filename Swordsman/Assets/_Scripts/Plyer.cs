using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plyer : MonoBehaviour
{
    private Camera _cam;
    private Vector3 _startMousePos, _currenMousePos, _directionMove;
    [SerializeField]
    private float _speedRotation, _speedMove;
    void Start()
    {
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
                transform.position += _directionMove * _speedMove;
            }
            //Debug.Log((_currenMousePos - _startMousePos));
        }

    }
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up * _speedRotation);
    }
}
