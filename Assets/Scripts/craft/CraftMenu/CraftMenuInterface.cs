using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace craft.CraftMenu
{
    public abstract class CraftMenuInterface : MonoBehaviour
    {
        public CraftObject craft;
        public Dictionary<GameObject, CraftSlot> slotsInCraftMenu;
        
        public abstract void CreateSlots();

        private void Start()
        {
            craft.InitCraft();
            CreateSlots();
        }
        
        /**
         * Метод ивентов для обработки событий под мышкой.
         */
        protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
        {
            EventTrigger trigger = obj.GetComponent<EventTrigger>();
            var eventTrigger = new EventTrigger.Entry();
            eventTrigger.eventID = type;
            eventTrigger.callback.AddListener(action);
            trigger.triggers.Add(eventTrigger);
        }
    }
}