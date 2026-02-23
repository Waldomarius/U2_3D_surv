using System.Collections.Generic;
using containers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace craft.MainMenu
{
    public abstract class MainMenuInterface : MonoBehaviour
    {
        [SerializeField] public GameObject container;
        
        protected ItemContainer item;
        public Dictionary<GameObject, ItemSlot> itemSlotsObject;
        
        public abstract void CreateSlots();

        private void Start()
        {
            item = container.GetComponent<ItemContainer>();
            item.InitItemContainer();
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