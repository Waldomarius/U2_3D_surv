using Items.scritableObjects.items;
using UnityEngine;

namespace Items.scritableObjects.dateBase
{
    [CreateAssetMenu(fileName = "Craft Group DataBase", menuName = "Items/DataBase/Craft Group DataBase")]
    public class CraftGroupDataBase : ScriptableObject, ISerializationCallbackReceiver
    {
        public ItemCraftGroupObject[] craftGroups;
        
        public void UpdateID()
        {
            for (int i = 0; i < craftGroups.Length; i++)
            {
                if (craftGroups != null && !craftGroups[i].data.id.Equals(i))
                {
                    craftGroups[i].data.id = i;
                }
            }
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            UpdateID();
        }
    }
}
