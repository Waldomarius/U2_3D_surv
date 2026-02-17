using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "New Craft Item", menuName = "Items/Item/New Craft Item")]
    public class ItemCraftObject : ScriptableObject
    {
        public string itemName;
        public Sprite uiDisplay;
        
        public ItemCraft data = new ItemCraft();

        public ItemCraft CreateItemCraft()
        {
            ItemCraft craft = new ItemCraft(this);
            return craft;
        }
    }
    
    [System.Serializable]
    public class ItemCraft
    {
        public int Id;
        public string itemName;
        public Sprite uiDisplay;
        
        public ItemCraft()
        {
            itemName = "";
            Id = -1;
        }

        public ItemCraft(ItemCraftObject item)
        {
            Id = item.data.Id;
            itemName = item.itemName;
            uiDisplay = item.uiDisplay;

        }
    }
}