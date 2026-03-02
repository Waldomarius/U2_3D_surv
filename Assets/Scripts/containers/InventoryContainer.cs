using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Items.scritableObjects.dateBase;
using Items.scritableObjects.items;
using UnityEngine;

namespace inventorySystem
{
    public class InventoryContainer : MonoBehaviour
    {
        [SerializeField] public ItemsDataBase dataBase;
        [SerializeField] public int _inventorySize = 20;
        private Inventory _container;
        
        public string savePath;
        
        public Action<bool> OnInventaryChanged;

        public Inventory GetContainer()
        {
            return _container;
        }

        public void UpdateInventory()
        {
            OnInventaryChanged?.Invoke(true);
        }

        public void InitItemContainer()
        {
            _container = new Inventory(_inventorySize);
            // Создадим список на количество крафтовых элементов в БД
            _container.items =  new List<InventorySlot>(dataBase.items.Length);

            for (int i = 0; i < _inventorySize; i++)
            {
                _container.items.Add(new InventorySlot());
            }
        }

        public bool AddItem(Item item, int amount)
        {
            InventorySlot slot = FindItemOnInventorySlot(item);
            
            if (!dataBase.items[item.id] || slot == null)
            {
                SetEmptySlot(item,  amount);
                UpdateInventory();
                return true;
            }
            slot.AddAmount(amount);
            UpdateInventory();
            return true;
        }

        private InventorySlot SetEmptySlot(Item item, int amount)
        {
            for (int i = 0; i < _container.items.Count; i++)
            {
                if (_container.items[i].item.id <= -1)
                {
                    _container.items[i].UpdateSlot(item, amount);

                    return _container.items[i];
                }
            }

            return null;
        }
        
        private InventorySlot FindItemOnInventorySlot(Item item)
        {
            for (int i = 0; i < _container.items.Count; i++)
            {
                if (_container.items[i].item.id == item.id)
                {
                    return _container.items[i];
                }
            }
            
            return null;
        }

        /**
         * Меняем слоты местами.
         */
        public void SwapItem(InventorySlot itemIn, InventorySlot itemOut)
        {
            InventorySlot temp = new InventorySlot(itemOut.item, itemOut.amount);
            itemOut.UpdateSlot(itemIn.item, itemIn.amount);
            itemIn.UpdateSlot(temp.item, temp.amount);
        }
        
        public void RemoveItem(Item item)
        {
            for (int i = 0; i < _container.items.Count; i++)
            {
                if (_container.items[i].item == item)
                {
                    // Убрали айтем из контейнера
                    _container.items[i].UpdateSlot(null, 0);
                }
            }
        }

        [ContextMenu("Inventory Save")]
        public void SaveInventory()
        {
            string saveData = JsonUtility.ToJson(this, true);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(string.Concat(Application.absoluteURL, savePath));
            bf.Serialize(file, saveData);
            file.Close();
        }

        [ContextMenu("Inventory Load")]
        public void LoadInventory()
        {
            if (File.Exists(string.Concat(Application.absoluteURL, savePath)))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(string.Concat(Application.absoluteURL, savePath), FileMode.Open);
                JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);

                file.Position = 0;
                // Inventory newContainer = JsonUtility.FromJson<Inventory>(bf.Deserialize(file).ToString());
                InventoryContainer obj = (InventoryContainer) bf.Deserialize(file);
                // file.Close();
                
                
                for (int i = 0; i < _container.items.Count; i++)
                {
                    _container.items[i].UpdateSlot(obj._container.items[i].item, obj._container.items[i].amount);
                }
                
                
                file.Close();
                
                UpdateInventory();
            }
        }
        
        [ContextMenu("Inventory Clear")]
        public void ClearInventory()
        {
            _container = new Inventory(_inventorySize);
        }
    }

    [Serializable]
    public class Inventory
    {
        public List<InventorySlot> items;

        public Inventory(int inventorySize)
        {
            items = new List<InventorySlot>(inventorySize);
        }
        
        public void Clear()
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i].UpdateSlot(new Item(), 0);
            }
        }
    }

    [Serializable]
    public class InventorySlot
    {
        public InventoryInterface parent;
        public Item item;
        public int amount;

        public ItemObject itemsObject
        {
            get
            {
                if (item.id >= 0)
                {
                    return parent.GetContainer().dataBase.items[item.id];
                }
                return null;
            }
        }
        
        public InventorySlot()
        {
            item = new Item();
            amount = 0;
        }
        
        public InventorySlot(Item item, int amount)
        {
            this.item = item;
            this.amount = amount;
        }

        public void UpdateSlot(Item item, int amount)
        {
            this.item = item;
            this.amount = amount;
        }
        
        public void RemoveItem()
        {
            item = new Item();
            amount = 0;
        }

        public void AddAmount(int value)
        {
            amount += value;
        }

        public bool CanPlaceInSlot(ItemObject itemsObject)
        {
            if (!itemsObject || itemsObject.data.id < 0)
            {
                return true;
            }

            return false;
        }
    }
}