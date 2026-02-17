using System.Collections.Generic;
using dateBase;
using Items;
using UnityEngine;

namespace craft.CraftMenu
{
    [CreateAssetMenu(fileName = "New Craft", menuName = "GameObject/New Craft")]
    public class CraftObject : ScriptableObject
    {
        public CraftDataBase dataBase;
        public Craft container;

        public void InitCraft()
        {
            // Инитим список на количество крафтовых групп в БД
            container.craftSlots =  new List<CraftSlot>(dataBase.crafts.Length);

            for (int i = 0; i < dataBase.crafts.Length; i++)
            {
                ItemCraft item = dataBase.crafts[i].CreateItemCraft();
                container.craftSlots.Add(new CraftSlot(item));
            }
        }
    }

    [System.Serializable]
    public class Craft
    {
        public List<CraftSlot> craftSlots;
    }
    
    [System.Serializable]
    public class CraftSlot
    {
        public ItemCraft item;

        public CraftSlot()
        {
            item = null;
        }
        
        public CraftSlot(ItemCraft item)
        {
            this.item = item;
        }
    }
}