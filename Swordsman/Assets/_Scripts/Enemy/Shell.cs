using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    [SerializeField]
    private SphereHalves[] _sphereHalves;
    [SerializeField]
    private ParticleSystem _bloodPS;

    [SerializeField]
    private float _foresePushHalves,_speed,_timeLife;

    private void Start()
    {
        Destroy(gameObject, _timeLife);
        Destroy(gameObject, _timeLife);
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _speed);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Sword" )
        {
            gameObject.layer = 12;
            Vector3 direction = (transform.position - collision.GetContact(0).point).normalized;

            foreach (var halves in _sphereHalves)
            {
                halves.gameObject.SetActive(true);
                halves.Push(direction, _foresePushHalves);
            }

            Destruction();
        }
    }
    private void Destruction()
    {
        PlayBlood();
        Destroy(gameObject);
    }
    private void PlayBlood()
    {
        if (_bloodPS != null)
        {
            _bloodPS.transform.SetParent(null);
            _bloodPS.Play();
            Destroy(_bloodPS, 1);
        }
    }
}
