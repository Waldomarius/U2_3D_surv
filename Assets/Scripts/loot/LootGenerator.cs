using System.Collections.Generic;
using Items.scritableObjects.items;
using UnityEngine;
using Random = UnityEngine.Random;

namespace loot
{
    public class LootGenerator : MonoBehaviour
    {
        [SerializeField] private float _maxX;
        [SerializeField] private float _minX;
        [SerializeField] private float _maxY;
        [SerializeField] private float _minY;

        [SerializeField] private LootElements[] _lootElements;

        private List<GameObject> _lootsOnLocation = new List<GameObject>();
        private void Awake()
        {
            StartLootGenerator();
        }

        private void StartLootGenerator()
        {
            foreach (LootElements loot in _lootElements)
            {
                for (int i = 0; i < loot.count; i++)
                {
                    float randomX = Random.Range(_minX, _maxX);
                    float randomY = Random.Range(_minY, _maxY);
                    
                    ItemObject item = loot.item;
                    Vector3 position = new Vector3( randomX, 0, randomY);
                    GameObject lootOnLocation = Instantiate(item.buildingPrefab, position,  Quaternion.identity);
                    // Устанавливаем родителя для текущего объекта
                    lootOnLocation.transform.SetParent(gameObject.transform);
                    
                    _lootsOnLocation.Add(lootOnLocation);
                }
            }
        }
    }
    
    [System.Serializable]
    public class LootElements
    {
        public ItemObject item;
        public int count;
    }
}