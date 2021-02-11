using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTower : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _enemies = new List<GameObject>();
    [SerializeField]
    private List<bool> _listEnemies;
    [SerializeField]
    private Enemy _spher, _spherRock;
    [SerializeField]
    private BoxCollider _boxCollider;

    private void Awake()
    {
        if (_enemies.Count <= 0)
            SpawnSpher();
        CanvasManager.QuantityEnemy += _enemies.Count;
    }
    void Start()
    {
        ColliderSettings();
    }
    [ContextMenu("SpawnSpher")]
    private void SpawnSpher()
    {
        if (_enemies.Count>0)
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                DestroyImmediate(_enemies[i]);
            }
            _enemies.Clear();
        }

        Vector3 PosSpawn = transform.position;
        PosSpawn.y -= 1;

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
    private void ColliderSettings()
    {
        Vector3 CenterCollider = _boxCollider.center;
        CenterCollider.y = (_enemies.Count - 1);
        _boxCollider.center = CenterCollider;
        Vector3 SizeCollider = _boxCollider.size;
        SizeCollider.y = (_enemies.Count) * 2;
        _boxCollider.size = SizeCollider;

    }

}
