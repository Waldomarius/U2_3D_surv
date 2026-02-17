using System.Collections.Generic;
using dateBase;
using Items;
using UnityEngine;

namespace craft.MainMenu
{
    [CreateAssetMenu(fileName = "New Craft Element", menuName = "GameObject/New Craft Element")]
    public class CraftElementObject : ScriptableObject
    {
        public CraftElementDataBase dataBase;
        public CraftElement container;
        
        public void InitCraftElement()
        {
            // Инитим список на количество крафтовых элементов в БД
            container.craftElementSlots =  new List<CraftElementSlot>(dataBase.craftElements.Length);

            for (int i = 0; i < dataBase.craftElements.Length; i++)
            {
                ItemCraftElement item = dataBase.craftElements[i].CreateItemCraftElement();
                container.craftElementSlots.Add(new CraftElementSlot(item));
            }
        }
    }
    
    [System.Serializable]
    public class CraftElement
    {
        public List<CraftElementSlot> craftElementSlots;
    }

    
    [System.Serializable]
    public class CraftElementSlot
    {
        public ItemCraftElement item;

        public CraftElementSlot()
        {
            item = null;
        }
        
        public CraftElementSlot(ItemCraftElement item)
        {
            this.item = item;
        }
    }
}