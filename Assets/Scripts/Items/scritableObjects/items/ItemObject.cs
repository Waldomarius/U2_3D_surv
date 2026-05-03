using craft.LeftMenu;
using UnityEngine;

namespace Items.scritableObjects.items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Items/Item/New Item")]
    public class ItemObject : ScriptableObject
    {
        public string itemName;
        public Sprite uiDisplay;
        public GroupType groupType;
        public GameObject buildingPrefab;
        public CraftElements[] craftElements;
        public float genarateCount = 0f;
        public Item data = new Item();
        public float activeMenuSlot;

        public Item CreateItem()
        {
            Item craftElement = new Item(this);
            return craftElement;
        }
    }

    [System.Serializable]
    public class CraftElements
    {
        public ItemObject item;
        public int count;
    }

    [System.Serializable]
    public class Item
    {
        public int id;
        public string itemName;
        public Sprite uiDisplay;
        public GroupType groupType;
        public GameObject buildingPrefab;
        public CraftElements[] craftElements;
        public float genarateCount;
        public float activeMenuSlot;
        
        public Item()
        {
            itemName = "";
            id = -1;
        }

        public Item(ItemObject item)
        {
            id = item.data.id;
            itemName = item.itemName;
            uiDisplay = item.uiDisplay;
            groupType = item.groupType;
            buildingPrefab =  item.buildingPrefab;
            craftElements  = item.craftElements;
            genarateCount = item.genarateCount;
            activeMenuSlot = item.activeMenuSlot;
        }
    }
}