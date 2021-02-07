using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTower : MonoBehaviour
{
    private List<GameObject> _enemies = new List<GameObject>();
    [SerializeField]
    private List<bool> _listEnemies;
    [SerializeField]
    private Enemy _spher, _spherRock;

    void Start()
    {
        SpawnSpher();
    }

    private void SpawnSpher()
    {
        Vector3 PosSpawn = transform.position;
        for (int i = 0; i < _listEnemies.Count; i++)
        {
            Enemy SpawnSpher = _listEnemies[i] ? _spherRock : _spher;
            Enemy Spher = Instantiate(SpawnSpher,PosSpawn,SpawnSpher.transform.rotation);
            Spher.transform.SetParent(transform);
            Spher.Initialization(this);
            _enemies.Add(Spher.gameObject);
            PosSpawn.y += 2;
        }
    }
    public void RemoveSpher(GameObject SpherObj)
    {
        _enemies.Remove(SpherObj);

        if (_enemies.Count<=0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, _spher.transform.localScale.z/2);
    }
}
