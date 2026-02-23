using craft.LeftMenu;
using UnityEngine;

namespace Items.scritableObjects.items
{
    [CreateAssetMenu(fileName = "New Craft Group Item", menuName = "Items/Item/New Craft Group Item")]
    public class ItemCraftGroupObject : ScriptableObject
    {
        public string itemName;
        public Sprite uiDisplay;
        public GroupType groupType;
        public ItemCraftGroup data = new ItemCraftGroup();

        public ItemCraftGroup CreateItemCraftGroup()
        {
            ItemCraftGroup  craftGroup = new ItemCraftGroup(this);
            return craftGroup;
        }
    }

    [System.Serializable]
    public class ItemCraftGroup
    {
        public int id;
        public string itemName;
        public Sprite uiDisplay;
        public GroupType groupType;
        
        public ItemCraftGroup()
        {
            itemName = "";
            id = -1;
        }

        public ItemCraftGroup(ItemCraftGroupObject item)
        {
            id = item.data.id;
            itemName = item.itemName;
            uiDisplay = item.uiDisplay;
            groupType = item.groupType;
        }
    }
}