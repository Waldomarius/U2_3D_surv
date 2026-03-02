using inventorySystem;
using Items.scritableObjects.items;
using UnityEngine;

namespace player
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private GameObject _inventoryGO;
    
        private InventoryContainer _container;
        private void Awake()
        {
            _container = _inventoryGO.GetComponent<InventoryContainer>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var loot = other.GetComponent<LootComponent>();

            if (loot != null)
            {
                Item item = new Item(loot.item);

                // добавляем в инвентарь и убираем с улицы
                if (_container.AddItem(item, 1))
                {
                    Destroy(other.gameObject);
                }
            }
        }
    }
}