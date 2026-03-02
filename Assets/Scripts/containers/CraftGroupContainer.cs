using System.Collections.Generic;
using Items.scritableObjects.dateBase;
using Items.scritableObjects.items;
using UnityEngine;

namespace containers
{
    public class CraftGroupContainer : MonoBehaviour
    {
        [SerializeField] private CraftGroupDataBase dataBase;
        private CraftGroupsContainer _container;

        public CraftGroupsContainer GetContainer()
        {
            return _container;
        }
        
        public void InitCraftGroup()
        {
            _container = new CraftGroupsContainer();
            // Создаем список на количество крафтовых групп в БД
            _container.craftGroupSlots = new List<CraftGroupSlot>(dataBase.craftGroups.Length);

            for (int i = 0; i < dataBase.craftGroups.Length; i++)
            {
                ItemCraftGroup item = dataBase.craftGroups[i].CreateItemCraftGroup();
                _container.craftGroupSlots.Add(new CraftGroupSlot(item));
            }
        }
    }
    
    [System.Serializable]
    public class CraftGroupsContainer
    {
        public List<CraftGroupSlot> craftGroupSlots;
    }
    
    [System.Serializable]
    public class CraftGroupSlot
    {
        public ItemCraftGroup item;

        public CraftGroupSlot()
        {
            item = null;
        }
        
        public CraftGroupSlot(ItemCraftGroup item)
        {
            this.item = item;
        }
    }
}