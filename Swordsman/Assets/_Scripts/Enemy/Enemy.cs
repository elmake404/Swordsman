using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Delimiter _delimiter;
    [SerializeField]
    private Rigidbody _rbMain;
    [SerializeField]
    private EnemyTower _tower;

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
            RemovalFromTower();
            _delimiter.Separation(collision.GetContact(0).point);
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
    public void Initialization(EnemyTower tower)
    {
        _tower = tower;
    }
}
