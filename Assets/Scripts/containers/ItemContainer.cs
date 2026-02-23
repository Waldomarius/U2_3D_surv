using System.Collections.Generic;
using Items.scritableObjects.dateBase;
using Items.scritableObjects.items;
using UnityEngine;

namespace containers
{
    public class ItemContainer : MonoBehaviour
    {
        public ItemsDataBase dataBase;
        
        private ItemSlotsContainer _container;

        public ItemSlotsContainer GetContainer()
        {
            return _container;
        } 
        
        public void InitItemContainer()
        {
            _container = new ItemSlotsContainer();
            // Создадим список на количество крафтовых элементов в БД
            _container.craftElementSlots =  new List<ItemSlot>(dataBase.craftElements.Length);

            for (int i = 0; i < dataBase.craftElements.Length; i++)
            {
                Item item = dataBase.craftElements[i].CreateItem();
                _container.craftElementSlots.Add(new ItemSlot(item));
            }
        }
    }
    
    [System.Serializable]
    public class ItemSlotsContainer
    {
        public List<ItemSlot> craftElementSlots;
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