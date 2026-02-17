using System.Collections.Generic;
using craft.CraftMenu;
using craft.LeftMenu;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace craft.MainMenu
{
    public class DynamicCraftMainMenuUI : MainMenuInterface
    {
        [SerializeField] private GameObject _craftElementPrefab;
        [SerializeField] private int X_START;
        [SerializeField] private int Y_START;
        [SerializeField] private int X_SPACE_BEETWEEN_ITEMS;
        [SerializeField] private int Y_SPACE_BEETWEEN_ITEMS;
        [SerializeField] private int NUMBER_OF_COLUMN;
        [SerializeField] private GameObject _dynamicCraftMenuUIObj;
        
        private DynamicCraftMenuUI _dynamicCraftMenuUI;
            
        private void Awake()
        {
            _dynamicCraftMenuUI = _dynamicCraftMenuUIObj.GetComponent<DynamicCraftMenuUI>();
        }
        
        public override void CreateSlots()
        {
            gameObject.SetActive(false);
            slotsInMainMenu = new Dictionary<GameObject, CraftElementSlot>();
            
            for (int i = 0; i < craftElement.container.craftElementSlots.Count; i++)
            {
                CraftElementSlot slot = craftElement.container.craftElementSlots[i];

                GameObject obj = Instantiate(_craftElementPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

                Image img = obj.GetComponentInChildren<Image>();
                img.sprite = slot.item.uiDisplay;

                TextMeshProUGUI text = obj.GetComponentInChildren<TextMeshProUGUI>();
                text.text = slot.item.itemName;

                // На каждый слот вешаем свой конкретный слушатель и свой конкретный триггер
                AddEvent(obj, EventTriggerType.PointerClick, delegate { OnPointerClick(obj);});
                
                obj.SetActive(false);
                slotsInMainMenu.Add(obj, slot);
            }
        }
        
        private void OnPointerClick(GameObject obj)
        {
            CraftElementSlot slot = slotsInMainMenu.GetValueOrDefault(obj, null);

            // Покажем меню согласно groupType объектов
            _dynamicCraftMenuUI.VisibleCraft(slot);
        }
        
        /**
        * Создадим список слотов для отображения на UI.
        */
        public void VisibleSlotsByType(CraftGroupSlot slot)
        {
            // Показываем фон меню 
            gameObject.SetActive(true);
            
            // Устанавливаем иконку и текст меню
            Image img = gameObject.transform.GetChild(0).GetComponentInChildren<Image>();
            img.sprite = slot.item.uiDisplay;

            TextMeshProUGUI text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
            text.text = slot.item.itemName;

            // Очистим старое меню
            foreach (KeyValuePair<GameObject, CraftElementSlot> pair in slotsInMainMenu)
            {
                pair.Key.SetActive(false);
            }
            
            int count = 0;

            // Засетаем новое меню
            foreach (KeyValuePair<GameObject, CraftElementSlot> pair in slotsInMainMenu)
            {
                CraftElementSlot temp = pair.Value;
                if (temp.item.groupType == slot.item.groupType)
                {
                    pair.Key.GetComponent<RectTransform>().localPosition = GetPosition(count);
                    pair.Key.SetActive(true);
                    count++;
                }
            }
        }

        /**
        * Просчитываем позицию каждого слога на канвасе.
        */
        private Vector3 GetPosition(int i)
        {
            return new Vector3(
                X_START + (X_SPACE_BEETWEEN_ITEMS * (i % NUMBER_OF_COLUMN)),
                Y_START + (-Y_SPACE_BEETWEEN_ITEMS * (i / NUMBER_OF_COLUMN)),
                0
            );
        }
    }
}