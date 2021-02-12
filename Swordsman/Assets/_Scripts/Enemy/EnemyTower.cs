using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTower : MonoBehaviour
{
    [System.Serializable]
    private struct EnemyCharacteristics
    {
        public bool Armored;
        public bool Quick;
    }

    [SerializeField]
    private List<Enemy> _enemies = new List<Enemy>();
    [SerializeField]
    private List<EnemyCharacteristics> _listEnemies;
    [SerializeField]
    private Enemy _spher;
    [SerializeField]
    private BoxCollider _boxCollider;

    private void Awake()
    {
        if (_listEnemies.Count != _enemies.Count)
            SpawnSpher();

        InitializationEnemy();
        CanvasManager.QuantityEnemy += _enemies.Count;
    }
    void Start()
    {
        ColliderSettings();
    }
    [ContextMenu("SpawnSpher")]
    private void SpawnSpher()
    {
        if (_enemies.Count > 0)
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                DestroyImmediate(_enemies[i].gameObject);
            }
            _enemies.Clear();
        }

        Vector3 PosSpawn = transform.position;
        PosSpawn.y -= 1;

        for (int i = 0; i < _listEnemies.Count; i++)
        {
            Enemy enemy = Instantiate(_spher, PosSpawn, _spher.transform.rotation);
            enemy.transform.SetParent(transform);
            _enemies.Add(enemy);
            PosSpawn.y += 2;
        }
    }
    private void InitializationEnemy()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].Initialization(this, _listEnemies[i].Armored, _listEnemies[i].Quick);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, _spher.transform.localScale.z / 2);
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
    public void RemoveSpher(Enemy enemy)
    {
        _enemies.Remove(enemy);

        if (_enemies.Count <= 0)
        {
            Destroy(gameObject);
        }
    }
    public Enemy EnemyIsOnTheGround()
    {
        return _enemies[0];
    }


}
