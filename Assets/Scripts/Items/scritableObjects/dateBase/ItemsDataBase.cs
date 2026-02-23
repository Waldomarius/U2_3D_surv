using Items.scritableObjects.items;
using UnityEngine;

namespace Items.scritableObjects.dateBase
{
    [CreateAssetMenu(fileName = "Items DataBase", menuName = "Items/DataBase/Items DataBase")]
    public class ItemsDataBase : ScriptableObject, ISerializationCallbackReceiver
    {
        public ItemObject[] craftElements;
        
        public void UpdateID()
        {
            for (int i = 0; i < craftElements.Length; i++)
            {
                if (craftElements != null && !craftElements[i].data.id.Equals(i))
                {
                    craftElements[i].data.id = i;
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