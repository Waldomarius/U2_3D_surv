using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace craft.MainMenu
{
    public abstract class MainMenuInterface : MonoBehaviour
    {
        public CraftElementObject craftElement;
        public Dictionary<GameObject, CraftElementSlot> slotsInMainMenu;
        
        public abstract void CreateSlots();

        private void Start()
        {
            craftElement.InitCraftElement();
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