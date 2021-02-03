using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTower : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private List<Enemy> _enemies;

    [SerializeField]
    private float _speedMove;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_target != null)
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _speedMove);
    }
}
