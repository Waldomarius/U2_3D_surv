using System.Collections.Generic;
using UnityEngine;

namespace enemy
{
    public class EnemyPool : MonoBehaviour
    {
        private List<GameObject> _pool = new List<GameObject>();
        private List<EnemySpawnConfig> _enemyData;

        public List<GameObject> GetPoolObjects() => _pool;

        private EnemyCreator _creator;
        
        private void Awake()
        {
            _creator = GetComponent<EnemyCreator>();
        }

        public void Initialize(List<EnemySpawnConfig> enemyData)
        {
            _enemyData = enemyData;
            InitializePool();
        }
        
        private void InitializePool()
        {
            foreach (EnemySpawnConfig data in _enemyData)
            {
                foreach (SpawnPointData pointData in data.spawnPoints)
                {
                    GameObject newObj = _creator.CreateNewObject(
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