using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plyer : MonoBehaviour
{
    public static Plyer Player;

    public delegate void NamberCill(int namber);
    public event NamberCill Cill;

    private Camera _cam;
    private Vector3 _startMousePos, _currenMousePos, _directionMove;
    [SerializeField]
    private float _speedRotationMax, _speedMoveMax, _rotationAcceleration;
    [SerializeField]
    [Range(0,100)]
    private float _speedLossPercentage;
    [SerializeField]
    private float _speedRotation;
    private void Awake()
    {
        Player = this;
    }
    void Start()
    {
        _speedRotation = _speedRotationMax;
        _cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cill?.Invoke(+1);
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
            _speedRotation =  _speedRotationMax - ((_speedRotationMax/100)*_speedLossPercentage);
            Debug.Log(_speedRotation);
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
