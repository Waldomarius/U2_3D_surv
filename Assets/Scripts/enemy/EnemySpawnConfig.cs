using System;
using System.Collections.Generic;
using UnityEngine;

namespace enemy
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
}