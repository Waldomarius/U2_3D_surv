using craft.LeftMenu;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "New Craft Element Item", menuName = "Items/Item/New Craft Element Item")]
    public class ItemCraftElementObject : ScriptableObject
    {
        public string itemName;
        public Sprite uiDisplay;
        public GroupType groupType;
        public GameObject buildingPrefab;
        public ItemCraftObject[] itemCraft;
        
        public ItemCraftElement data = new ItemCraftElement();

        public ItemCraftElement CreateItemCraftElement()
        {
            ItemCraftElement craftElement = new ItemCraftElement(this);
            return craftElement;
        }
    }
    
    [System.Serializable]
    public class ItemCraftElement
    {
        public int Id;
        public string itemName;
        public Sprite uiDisplay;
        public GroupType groupType;
        public GameObject buildingPrefab;
        public ItemCraftObject[] itemCraft;
        
        public ItemCraftElement()
        {
            itemName = "";
            Id = -1;
        }

        public ItemCraftElement(ItemCraftElementObject item)
        {
            Id = item.data.Id;
            itemName = item.itemName;
            uiDisplay = item.uiDisplay;
            groupType = item.groupType;
            buildingPrefab =  item.buildingPrefab;
            itemCraft  = item.itemCraft;
        }
    }
}