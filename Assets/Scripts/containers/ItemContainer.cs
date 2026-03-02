using System.Collections.Generic;
using Items.scritableObjects.dateBase;
using Items.scritableObjects.items;
using UnityEngine;

namespace containers
{
    public class ItemContainer : MonoBehaviour
    {
        [SerializeField] private ItemsDataBase dataBase;
        private ItemCraftSlotsContainer _container;

        public ItemCraftSlotsContainer GetContainer()
        {
            return _container;
        } 
        
        public void InitItemContainer()
        {
            _container = new ItemCraftSlotsContainer();
            // Создадим список на количество крафтовых элементов в БД
            _container.itemSlots =  new List<ItemSlot>(dataBase.items.Length);

            for (int i = 0; i < dataBase.items.Length; i++)
            {
                Item item = dataBase.items[i].CreateItem();
                _container.itemSlots.Add(new ItemSlot(item));
            }
        }
    }
    
    [System.Serializable]
    public class ItemCraftSlotsContainer
    {
        public List<ItemSlot> itemSlots;
    }

    
    [System.Serializable]
    public class ItemSlot
    {
        public Item item;

        public ItemSlot()
        {
            item = null;
        }
        
        public ItemSlot(Item item)
        {
            this.item = item;
        }
    }
}