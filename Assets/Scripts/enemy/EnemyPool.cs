using System.Collections.Generic;
using UnityEngine;

namespace enemy
{
    public class EnemyPool : MonoBehaviour
    {
        private List<GameObject> _pool = new List<GameObject>();
        private List<EnemySpawner.EnemySpawnConfig> _enemyData;

        public List<GameObject> GetPoolObjects() => _pool;

        private EnemyFactory _factory;
        
        private void Awake()
        {
            _factory = GetComponent<EnemyFactory>();
        }

        public void Initialize(List<EnemySpawner.EnemySpawnConfig> enemyData)
        {
            _enemyData = enemyData;
            InitializePool();
        }
        
        private void InitializePool()
        {
            foreach (EnemySpawner.EnemySpawnConfig data in _enemyData)
            {
                foreach (EnemySpawner.SpawnPointData pointData in data.spawnPoints)
                {
                    GameObject newObj = _factory.CreateNewObject(
                        data.enemyPrefab,
                        pointData.position,
                        data.moveSpeed,
                        data.damage);
                    _pool.Add(newObj);
                }
            }
        }
    }
}