using System.Collections;
using inventorySystem;
using Items.scritableObjects.items;
using loot;
using UnityEngine;

namespace player
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private GameObject _inventoryGO;
    
        private InventoryContainer _container;
        private Camera _camera;
        private void Awake()
        {
            _container = _inventoryGO.GetComponent<InventoryContainer>();
            _camera = Camera.main;
        }

        private void OnTriggerEnter(Collider other)
        {
            var loot = other.GetComponent<LootComponent>();

            if (loot != null)
            {
                SetUnactiveLootObject(loot, other.gameObject);
            }
        }

        private void SetUnactiveLootObject(LootComponent loot, GameObject lootObject)
        {
            Item item = new Item(loot.item);

            // добавляем в инвентарь и убираем с улицы
            if (_container.AddItem(item, 1))
            {
                lootObject.SetActive(false);
                StartCoroutine(SetActiveLootObject(lootObject));
            }
        }
        
        private IEnumerator SetActiveLootObject(GameObject lootObject)
        {
            yield return new WaitForSeconds(5f);
            lootObject.SetActive(true);
        }

    }
}