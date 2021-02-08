using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public static Transform PlayerTransform;

    [SerializeField]
    private Rigidbody _rbMain;
    private Camera _cam;
    private Vector3 _startMousePos, _currenMousePos, _directionMove;
    private Vector3 _directionRotation = Vector3.up;

    [SerializeField]
    private float _speedRotationMax, _rotationAcceleration, _speedMoveMax;
    [SerializeField]
    [Range(0, 100)]
    private float _speedLossPercentage;
    private float _speedRotation;
    private void Awake()
    {
        PlayerTransform = transform;
    }
    private void Start()
    {
        _cam = Camera.main;
        _speedRotation = _speedRotationMax;
    }

    private void Update()
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
            if ((_currenMousePos - _startMousePos).sqrMagnitude > 0.0001f)
            {
                Vector3 directionMose = (_currenMousePos - _startMousePos).normalized;

                _directionMove.x = directionMose.x;
                _directionMove.z = directionMose.y;
                transform.position += _directionMove * _speedMoveMax;
            }
        }
    }

    private void FixedUpdate()
    {
        _speedRotation = Mathf.Lerp(_speedRotation, _speedRotationMax, _rotationAcceleration);
        transform.Rotate(_directionRotation * _speedRotation);
        //_rbMain.velocity = Vector3.zero;
    }
    public void SpeedCut()
    {
        _speedRotation = _speedRotationMax - ((_speedRotationMax / 100) * _speedLossPercentage);
    }
    public void ChangeDirectionRotation()
    {
        _directionRotation.y *= -1;
    }

}
