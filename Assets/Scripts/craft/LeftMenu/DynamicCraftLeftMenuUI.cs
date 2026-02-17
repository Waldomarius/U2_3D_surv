using System.Collections.Generic;
using craft.MainMenu;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace craft.LeftMenu
{
    public class DynamicCraftLeftMenuUI : LeftMenuInterface
    {
        [SerializeField] private GameObject _craftGroupPrefab;
        [SerializeField] private int X_START;
        [SerializeField] private int Y_START;
        [SerializeField] private int X_SPACE_BEETWEEN_ITEMS;
        [SerializeField] private int Y_SPACE_BEETWEEN_ITEMS;
        [SerializeField] private int NUMBER_OF_COLUMN;
        [SerializeField] private GameObject _dunamicCraftMainMenuUIObj;

        private DynamicCraftMainMenuUI _dynamicCraftMainMenuUI;
        private void Awake()
        {
            _dynamicCraftMainMenuUI = _dunamicCraftMainMenuUIObj.GetComponent<DynamicCraftMainMenuUI>();
        }

        /**
         * Создадим список слотов для отображения на UI.
         */
        public override void CreateSlots()
        {
            slotsInLeftMenu = new Dictionary<GameObject, CraftGroupSlot>();

            for (int i = 0; i < craftGroup.container.craftGroupSlots.Count; i++)
            {
                GameObject obj = Instantiate(_craftGroupPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

                CraftGroupSlot slot = craftGroup.container.craftGroupSlots[i];
                
                Image img = obj.GetComponentInChildren<Image>();
                img.sprite = slot.item.uiDisplay;
                
                // На каждый слот вешаем свой конкретный слушатель и свой конкретный триггер
                AddEvent(obj, EventTriggerType.PointerClick, delegate { OnPointerClick(obj);});
                
                slotsInLeftMenu.Add(obj, slot);
            }
        }

        private void OnPointerClick(GameObject obj)
        {
            CraftGroupSlot slot = slotsInLeftMenu.GetValueOrDefault(obj, null);

            // Покажем меню согласно groupType объектов
            _dynamicCraftMainMenuUI.VisibleSlotsByType(slot);
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