using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{    [SerializeField]
    private SphereHalves[] _sphereHalves;

    [SerializeField]
    private float _foresePush, _timeBeforeDestroy;

    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Sword")
        {
            gameObject.layer = 12;
            Vector3 direction = (transform.position-collision.GetContact(0).point).normalized;
            foreach (var halves in _sphereHalves)
            {
                halves.Push(direction, _foresePush);
            }
            Destroy(gameObject, _timeBeforeDestroy);
            enabled = false;

        }
    }
}
