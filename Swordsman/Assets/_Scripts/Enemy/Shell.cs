using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    [SerializeField]
    private Delimiter _delimiter;
    [SerializeField]
    private ParticleSystem _bloodPS;

    [SerializeField]
    private float _speed,_timeLife;

    private void Start()
    {
        Destroy(gameObject, _timeLife);
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _speed);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Sword" 
            || collision.collider.tag == "Wall")
        {
            gameObject.layer = 12;
            _delimiter.Separation(collision.GetContact(0).point);
        }
    }
}
