using System;
using System.Collections.Generic;
using UnityEngine;

namespace enemy
{
    public class EnemySpawner : MonoBehaviour
    {


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