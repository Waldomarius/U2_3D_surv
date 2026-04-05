using System.Collections.Generic;
using Items.scritableObjects.dateBase;
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

        [SerializeField] private ItemsDataBase _itemsDataBase;
        [SerializeField] private LayerMask _groundLayer;

        private List<GameObject> _lootsOnLocation = new List<GameObject>();
        private void Awake()
        {
            StartLootGenerator();
        }

        private void StartLootGenerator()
        {
            foreach (ItemObject item in _itemsDataBase.items)
            {
                if (item.genarateCount == 0)
                {
                    continue;
                }
                
                for (int i = 0; i < item.genarateCount; i++)
                {
                    float randomX = Random.Range(_minX, _maxX);
                    float randomY = Random.Range(_minY, _maxY);
                    
                    float tempZPos = 5;
                    Vector3 position = new Vector3(randomX, tempZPos, randomY);
                    
                    RaycastHit hit;
                    Ray downRay = new Ray(position, -Vector3.up);
                    
                    if (Physics.Raycast(downRay, out hit, _groundLayer))
                    {
                        position = new Vector3(randomX, tempZPos - hit.distance , randomY);
                    }
                    
                    GameObject lootOnLocation = Instantiate(item.buildingPrefab, position, Quaternion.identity);
                    // Устанавливаем родителя для текущего объекта
                    lootOnLocation.transform.SetParent(gameObject.transform);
                    
                    _lootsOnLocation.Add(lootOnLocation);
                }
            }
        }
    }
}