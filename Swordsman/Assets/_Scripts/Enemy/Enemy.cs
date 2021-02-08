using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private SphereHalves[] _sphereHalves;
    [SerializeField]
    private ParticleSystem _bloodPS;
    [SerializeField]
    private Rigidbody _rbMain;
    private EnemyTower _tower;

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

            Destruction();
        }

        if (collision.gameObject.layer == 9)
        {
            gameObject.layer = 11;

            IsActive = true;
            if (tag == "Rock")
            {
                RemovalFromTower();
                transform.SetParent(null);
            }
        }
    }
    private void RemovalFromTower()
    {
        if (_tower != null)
        {
            _tower.RemoveSpher(gameObject);
            _tower = null;
        }
    }
    private void Destruction()
    {
        PlayBlood();
        RemovalFromTower();
        Destroy(gameObject, _timeBeforeDestroy);
        enabled = false;
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
    public void Initialization(EnemyTower tower)
    {
        _tower = tower;
    }
}
