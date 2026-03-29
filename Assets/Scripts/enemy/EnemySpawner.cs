using System;
using System.Collections.Generic;
using UnityEngine;

namespace enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        public enum EnemyType
        {
            Zombie,
        }
        
        [Serializable]
        public class EnemySpawnConfig
        {
            public EnemyType enemyType;
            public GameObject enemyPrefab;
            public float moveSpeed = 2f;
            public float damage = 20f;
            
            public List<SpawnPointData> spawnPoints = new List<SpawnPointData>();
        }
        
        [Serializable]
        public class SpawnPointData
        {
            public Vector2 position;
        }
        
        [Header("Spawn Points")]
        [SerializeField] private List<EnemySpawnConfig> _spawnConfigs = new List<EnemySpawnConfig>();

        private EnemyPool _pool;

        private void Awake()
        {

            _pool = GetComponent<EnemyPool>();
            CreateAllEnemies();
        }
        
        private void CreateAllEnemies()
        {
            Debug.Log($"Creating all enemies.............");
            _pool.Initialize(_spawnConfigs);
        }
        
    }
}