using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private SphereHalves[] _sphereHalves;
    [SerializeField]
    private Rigidbody _rbMain;

    [SerializeField]
    private float _foresePush, _timeBeforeDestroy;
    public bool IsActive { get; private set; }

    void Start()
    {

    }
    private void FixedUpdate()
    {
        if (!IsActive)
        {
            _rbMain.AddForce(Vector3.down * 100);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Sword" && IsActive)
        {
            gameObject.layer = 12;
            Vector3 direction = (transform.position - collision.GetContact(0).point).normalized;
            foreach (var halves in _sphereHalves)
            {
                halves.gameObject.SetActive(true);
                halves.Push(direction, _foresePush);
            }

            Destroy(gameObject, _timeBeforeDestroy);
            enabled = false;
        }

        if (collision.gameObject.layer == 9)
        {
            gameObject.layer = 11;

            IsActive = true;
        }
    }
}
