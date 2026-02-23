using System.Collections.Generic;
using containers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace craft.LeftMenu
{
    public abstract class LeftMenuInterface : MonoBehaviour
    {
        [SerializeField] public GameObject container;
        
        protected CraftGroupContainer craftGroup;
        public Dictionary<GameObject, CraftGroupSlot> slotsInLeftMenu;
        
        public abstract void CreateSlots();

        private void Start()
        {
            craftGroup = container.GetComponent<CraftGroupContainer>();
            craftGroup.InitCraftGroup();
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