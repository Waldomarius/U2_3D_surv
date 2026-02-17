using System.Collections.Generic;
using dateBase;
using Items;
using UnityEngine;

namespace craft.LeftMenu
{
    [CreateAssetMenu(fileName = "New Craft Group", menuName = "GameObject/New Craft Group")]
    public class CraftGroupObject : ScriptableObject
    {
        public CraftGroupDataBase dataBase;
        public CraftGroup container;

        public void InitCraftGroup()
        {
            // Инитим список на количество крафтовых групп в БД
            container.craftGroupSlots =  new List<CraftGroupSlot>(dataBase.craftGroups.Length);

            for (int i = 0; i < dataBase.craftGroups.Length; i++)
            {
                ItemCraftGroup item = dataBase.craftGroups[i].CreateItemCraftGroup();
                container.craftGroupSlots.Add(new CraftGroupSlot(item));
            }
        }
    }
    
    [System.Serializable]
    public class CraftGroup
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