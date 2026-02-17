using craft.LeftMenu;
using UnityEngine;

namespace Items
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
        public int Id;
        public string itemName;
        public Sprite uiDisplay;
        public GroupType groupType;
        
        public ItemCraftGroup()
        {
            itemName = "";
            Id = -1;
        }

        public ItemCraftGroup(ItemCraftGroupObject item)
        {
            Id = item.data.Id;
            itemName = item.itemName;
            uiDisplay = item.uiDisplay;
            groupType = item.groupType;
        }
    }
}