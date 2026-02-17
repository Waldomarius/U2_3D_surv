using System.Collections.Generic;
using building;
using craft.MainMenu;
using eventSystem;
using Items;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace craft.CraftMenu
{
    public class DynamicCraftMenuUI : CraftMenuInterface
    {
        [SerializeField] private GameObject _craftPrefab;
        [SerializeField] private GameObject _createButtonPrefab;
        [SerializeField] private int X_START;
        [SerializeField] private int Y_START;
        [SerializeField] private int X_SPACE_BEETWEEN_ITEMS;
        [SerializeField] private int Y_SPACE_BEETWEEN_ITEMS;
        [SerializeField] private int NUMBER_OF_COLUMN;
        [SerializeField] private int X_CREATE_BUTTON;
        [SerializeField] private int Y_CREATE_BUTTON;
        [SerializeField] private GameObject _buildingSpawn;
        
        private GameObject _buildingPrefab;
        private BuildingController _buildingController;
        
        private void Awake()
        {
            _buildingController = _buildingSpawn.GetComponent<BuildingController>();
        }
        
        /**
         * Создадим список слотов для отображения на UI.
         */
        public override void CreateSlots()
        {
            gameObject.SetActive(false);
            
            // Объект кнопки создания крафта
            GameObject createButton = Instantiate(_createButtonPrefab, Vector3.zero, Quaternion.identity, transform);
            createButton.GetComponent<RectTransform>().localPosition = new Vector3(X_CREATE_BUTTON, Y_CREATE_BUTTON, 0);
            // На кнопу вешаем свой конкретный слушатель и свой конкретный триггер
            AddEvent(createButton, EventTriggerType.PointerClick, delegate { OnPointerClick(_buildingPrefab);});
            
            slotsInCraftMenu = new Dictionary<GameObject, CraftSlot>();

            for (int i = 0; i < craft.container.craftSlots.Count; i++)
            {
                GameObject obj = Instantiate(_craftPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

                CraftSlot slot = craft.container.craftSlots[i];
                
                Image img = obj.GetComponentInChildren<Image>();
                img.sprite = slot.item.uiDisplay;
                
                TextMeshProUGUI text = obj.GetComponentInChildren<TextMeshProUGUI>();
                text.text = slot.item.itemName;
                
                slotsInCraftMenu.Add(obj, slot);
            }
        }
        
        private void OnPointerClick(GameObject obj)
        {
            // Создадим крафт в мире
            _buildingController.CreateBuilding(obj);
            // Оповестим слушателей о закрытии UI
            GameEvents.CloseUI(true);
        }
        
        public void VisibleCraft(CraftElementSlot slot)
        {
            _buildingPrefab = null;
            // Показываем фон меню 
            gameObject.SetActive(true);
            
            // Устанавливаем иконку и текст меню
            Image img = gameObject.transform.GetChild(0).GetComponentInChildren<Image>();
            img.sprite = slot.item.uiDisplay;

            TextMeshProUGUI text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
            text.text = slot.item.itemName;
            
            // Очистим старое меню
            foreach (KeyValuePair<GameObject, CraftSlot> pair in slotsInCraftMenu)
            {
                pair.Key.SetActive(false);
            }

            _buildingPrefab = slot.item.buildingPrefab;
            
            int count = 0;

            // Засетаем новое меню
            ItemCraftObject[] itemCraft = slot.item.itemCraft;
            foreach (ItemCraftObject item in itemCraft)
            {
                foreach (KeyValuePair<GameObject, CraftSlot> pair in slotsInCraftMenu)
                {
                    CraftSlot temp = pair.Value;
                    if (item.itemName == temp.item.itemName)
                    {
                        pair.Key.GetComponent<RectTransform>().localPosition = GetPosition(count);
                        pair.Key.SetActive(true);
                        count++;
                        break;
                    }
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