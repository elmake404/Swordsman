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
    private float _speedRotationMax,_speedRotationMin, _speedBoostRotation, _speedPenaltyRotation, _timeRage, _rotationSlowdown, _speedMoveMax;
    [SerializeField]
    [Range(0, 100)]
    private float _speedLossPercentage;

    private float _speedRotation,_timerRage;
    private void Awake()
    {
        PlayerTransform = transform;
    }
    private void Start()
    {
        _cam = Camera.main;
        _speedRotation = _speedRotationMin;
    }

    private void Update()
    {
        if (CanvasManager.IsGameFlow)
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
                    transform.position += _directionMove * _speedMoveMax*Time.deltaTime;
                }
            }
            RageCount();
        }
    }

    private void FixedUpdate()
    {
        if (CanvasManager.IsGameFlow)
        {
            if (_timerRage <= 0)
                _speedRotation = Mathf.Lerp(_speedRotation, _speedRotationMin, _rotationSlowdown);
            else
                _timerRage -= Time.deltaTime;

            transform.Rotate(_directionRotation * _speedRotation);
        }
    }
    private void RageCount()
    {
        float factor = 1f / (_speedRotationMax - _speedRotationMin);
        CanvasManager.Instance.Rage((_speedRotation-_speedRotationMin)*factor);
    }
    public void SpeedCut()
        => _speedRotation = _speedRotationMax - ((_speedRotationMax / 100) * _speedLossPercentage);
    public void ChangeDirectionRotation() 
        => _directionRotation.y *= -1;
    public void AddSpeedRotation()
    {
        _speedRotation += _speedBoostRotation;
        _timerRage = _timeRage;
        if (_speedRotation>_speedRotationMax)
        {
            _speedRotation = _speedRotationMax;
        }
    }
    public void TakeAwaySpeedRotation()
    {
        _speedRotation += _speedPenaltyRotation;
        if (_speedRotation < _speedRotationMin)
        {
            _speedRotation = _speedRotationMin;
        }

    }

}
