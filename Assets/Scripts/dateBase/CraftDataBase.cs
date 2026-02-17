using Items;
using UnityEngine;

namespace dateBase
{
    [CreateAssetMenu(fileName = "Craft DataBase", menuName = "Items/DataBase/Craft DataBase")]
    public class CraftDataBase : ScriptableObject, ISerializationCallbackReceiver
    {
        public ItemCraftObject[] crafts;
        
        public void UpdateID()
        {
            for (int i = 0; i < crafts.Length; i++)
            {
                if (crafts != null && !crafts[i].data.Id.Equals(i))
                {
                    crafts[i].data.Id = i;
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