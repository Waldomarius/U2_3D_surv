using System.Collections.Generic;
using building;
using containers;
using eventSystem;
using inventorySystem;
using Items.scritableObjects.items;
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
        [SerializeField] private GameObject _createInactiveButtonPrefab;
        [SerializeField] private int X_START;
        [SerializeField] private int Y_START;
        [SerializeField] private int X_SPACE_BEETWEEN_ITEMS;
        [SerializeField] private int Y_SPACE_BEETWEEN_ITEMS;
        [SerializeField] private int NUMBER_OF_COLUMN;
        [SerializeField] private int X_CREATE_BUTTON;
        [SerializeField] private int Y_CREATE_BUTTON;
        [SerializeField] private GameObject _buildingSpawn;
        [SerializeField] public GameObject _inventoryContainerGO;
        
        protected InventoryContainer _inventoryContainer;
        
        private GameObject _buildingPrefab;
        private BuildingController _buildingController;
        private Dictionary<string, int> _craftElement = new Dictionary<string, int>();
        private GameObject _createButton;
        
        
        private void OnEnable()
        {
            _inventoryContainer.OnInventaryChanged += UpdateButtonUI;
        }

        private void OnDisable()
        {
            _inventoryContainer.OnInventaryChanged -= UpdateButtonUI;
        }
        
        private void Awake()
        {
            _inventoryContainer = _inventoryContainerGO.GetComponent<InventoryContainer>();
            _buildingController = _buildingSpawn.GetComponent<BuildingController>();
        }
        
        /**
         * Создадим список слотов для отображения на UI.
         */
        public override void CreateSlots()
        {
            gameObject.SetActive(false);

            itemSlotsObject = new Dictionary<GameObject, ItemSlot>();
            
            for (int i = 0; i < item.GetContainer().itemSlots.Count; i++)
            {
                ItemSlot slot = item.GetContainer().itemSlots[i];

                GameObject obj = Instantiate(_craftPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

                Image img = obj.GetComponentInChildren<Image>();
                img.sprite = slot.item.uiDisplay;

                TextMeshProUGUI text = obj.GetComponentInChildren<TextMeshProUGUI>();
                text.text = slot.item.itemName;
                
                obj.SetActive(false);
                itemSlotsObject.Add(obj, slot);
            }
        }

        private void UpdateButtonUI(bool obj)
        {
            CreateButton();
        }
        
        private void CreateButton()
        {
            Destroy(_createButton);
            
            if (IsApplyCraft())
            {
                _createButton = Instantiate(_createButtonPrefab, Vector3.zero, Quaternion.identity, transform);
            }
            else
            {
                _createButton = Instantiate(_createInactiveButtonPrefab, Vector3.zero, Quaternion.identity, transform);
            }
            _createButton.GetComponent<RectTransform>().localPosition = new Vector3(X_CREATE_BUTTON, Y_CREATE_BUTTON, 0);
            // На кнопу вешаем свой конкретный слушатель и свой конкретный триггер
            AddEvent(_createButton, EventTriggerType.PointerClick, delegate { OnPointerClick(_buildingPrefab);});
        }

        private void OnPointerClick(GameObject obj)
        {
            if (IsApplyCraft())
            {
                // Создадим крафт в мире
                _buildingController.CreateBuilding(obj);
                // Оповестим слушателей о закрытии UI
                GameEvents.CloseUI(true);
                GameEvents.OpenedUI(false);
                CreateButton();
                RemoveItemFromStorage();
                _inventoryContainer.UpdateInventory();
            }
        }

        /**
         * списать ресурсы со склада
         */
        private void RemoveItemFromStorage()
        {
            Inventory inventory = _inventoryContainer.GetContainer();
            
            foreach (KeyValuePair<string, int> pair in _craftElement)
            {
                foreach (var slot in inventory.items)
                {
                    if (pair.Key == slot.item.itemName)
                    {
                        int newStorageAmount = slot.amount -  pair.Value;
                        slot.amount =  newStorageAmount;
                        if (slot.amount == 0)
                        {
                            slot.RemoveItem();
                        }
                    }
                }
            }
        }
        
        /**
         * Надо проверить что на складе есть доступные ресурсы.
         */
        private bool IsApplyCraft()
        {
            Inventory inventory = _inventoryContainer.GetContainer();
            bool applyCraft = false;
            foreach (KeyValuePair<string, int> pair in _craftElement)
            {
                foreach (var slot in inventory.items)
                {
                    if (pair.Key == slot.item.itemName && pair.Value == slot.amount)
                    {
                        applyCraft = true;
                    }
                }

                if (!applyCraft)
                {
                    return false;
                }
            }
            
            return true;
        }
        
        public void VisibleCraft(ItemSlot slot)
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
            foreach (KeyValuePair<GameObject, ItemSlot> pair in itemSlotsObject)
            {
                pair.Key.SetActive(false);
            }
            
            _buildingPrefab = slot.item.buildingPrefab;

            // Удалим элементы для крафта с предыдущего крафта
            _craftElement.Clear();
            
            int count = 0;

            // Засетаем новое меню
            CraftElements[] itemCraft = slot.item.craftElements;
            foreach (CraftElements element in itemCraft)
            {
                foreach (KeyValuePair<GameObject, ItemSlot> pair in itemSlotsObject)
                {
                    ItemSlot temp = pair.Value;
                    if (element.item.itemName == temp.item.itemName)
                    {
                        // Положим элементы для крафта в словарь
                        _craftElement[element.item.itemName] = element.count;
                        
                        pair.Key.GetComponent<RectTransform>().localPosition = GetPosition(count);
                        pair.Key.SetActive(true);

                        TextMeshProUGUI value = pair.Key.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>();
                        value.text = element.count.ToString();
                        count++;
                        break;
                    }
                }
            }
            
            // Объект кнопки создания крафта
            CreateButton();
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