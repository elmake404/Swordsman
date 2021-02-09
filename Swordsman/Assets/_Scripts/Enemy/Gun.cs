using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private Shell _shell;
    [SerializeField]
    private Transform _shootPos;
    private Transform _target;

    [SerializeField]
    private float _speedRotation,_timeShoot, _activationZoneRadius;
    private float _sqrActivationZoneRadius , _timeShootСhanging;


    private void Start()
    {
        _target = PlayerMove.PlayerTransform;
        _sqrActivationZoneRadius = (_activationZoneRadius * _activationZoneRadius);

        //StartCoroutine(Fire());
    }

    private void FixedUpdate()
    {
        if (_target!=null)
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
            Destroy(gameObject);

            //gameObject.layer = 12;
            //Vector3 direction = (transform.position - collision.GetContact(0).point).normalized;

            //foreach (var halves in _sphereHalves)
            //{
            //    halves.gameObject.SetActive(true);
            //    halves.Push(direction, _foresePushHalves);
            //}

            //Destruction();
        }
    }
    //private void Destruction()
    //{
    //    PlayBlood();
    //    Destroy(gameObject);
    //}
    //private void PlayBlood()
    //{
    //    if (_bloodPS != null)
    //    {
    //        _bloodPS.transform.SetParent(null);
    //        _bloodPS.Play();
    //        Destroy(_bloodPS, 1);
    //    }
    //}

    private IEnumerator Fire()
    {
        if (_timeShoot <= 0)
        {
            _timeShoot = 1;
        }
        while (true)
        {
            Instantiate(_shell, _shootPos.position, _shootPos.rotation);
            yield return new WaitForSeconds(_timeShoot);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _activationZoneRadius);
    }
    private bool RotationGan()
    {
        Vector3 PosTarget = new Vector3(_target.position.x, transform.position.y, _target.position.z);
        Quaternion rotation = Quaternion.LookRotation(PosTarget - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation,rotation,_speedRotation);

        return (transform.rotation.eulerAngles - rotation.eulerAngles).magnitude <= 1.3f;
    }
}
