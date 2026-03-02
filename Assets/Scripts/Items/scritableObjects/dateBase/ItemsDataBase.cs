using Items.scritableObjects.items;
using UnityEngine;

namespace Items.scritableObjects.dateBase
{
    [CreateAssetMenu(fileName = "Items DataBase", menuName = "Items/DataBase/Items DataBase")]
    public class ItemsDataBase : ScriptableObject, ISerializationCallbackReceiver
    {
        public ItemObject[] items;
        
        public void UpdateID()
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items != null && !items[i].data.id.Equals(i))
                {
                    items[i].data.id = i;
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