using Items;
using UnityEngine;

namespace dateBase
{
    [CreateAssetMenu(fileName = "Craft Element DataBase", menuName = "Items/DataBase/Craft Element DataBase")]
    public class CraftElementDataBase : ScriptableObject, ISerializationCallbackReceiver
    {
        public ItemCraftElementObject[] craftElements;
        
        public void UpdateID()
        {
            for (int i = 0; i < craftElements.Length; i++)
            {
                if (craftElements != null && !craftElements[i].data.Id.Equals(i))
                {
                    craftElements[i].data.Id = i;
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