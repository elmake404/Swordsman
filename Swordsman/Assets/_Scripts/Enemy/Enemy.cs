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
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private SkinnedMeshRenderer _mesh;
    [SerializeField]
    private Material _quickMateril;
    [SerializeField]
    private Helmet _helmet;

    [SerializeField]
    private float _speedQuickMultiplier;
    private int _health;

    [HideInInspector]
    public float SpeedMultiplier;
    public bool IsActive { get; private set; }

    void Start()
    {
        StartCoroutine(StopAnomator());
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
            if (_health <= 0)
            {
                RemovalFromTower();
                CanvasManager.Instance.AddProgress();
                _delimiter.Separation(collision.GetContact(0).point);
            }
            else
            {
                _helmet.PushHelmet(collision.GetContact(0).point);
                //_helmet.gameObject.SetActive(false);
                _health -= 1;
            }
        }

        if (collision.gameObject.layer == 9)
        {
            gameObject.layer = 11;

            IsActive = true;
            //if (tag == "Rock")
            //{
            //    RemovalFromTower();
            //    transform.SetParent(null);
            //}
        }
    }
    private void RemovalFromTower()
    {
        if (_tower != null)
        {
            _tower.RemoveSpher(this);
            _tower = null;
        }
    }
    private IEnumerator StopAnomator()
    {
        _animator.enabled = false;
        yield return new WaitForSeconds(Random.Range(0.1f, 0.7f));
        _animator.enabled = true;
    }
    public void Initialization(EnemyTower tower, bool armored, bool quick)
    {
        _tower = tower;
        SpeedMultiplier = 1;

        if (armored)
        {
            if (_helmet)
            {
                _helmet.gameObject.SetActive(true);
            }
            _health++;
        }
        else if (quick)
        {
            if (_quickMateril != null)
            {
                _mesh.material = _quickMateril;
            }
            SpeedMultiplier = _speedQuickMultiplier;
        }
    }
}
