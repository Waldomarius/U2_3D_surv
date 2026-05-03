using System.Collections.Generic;
using eventSystem;
using Items.scritableObjects.items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace activeMenu
{
    public class ActiveMenuIU : MonoBehaviour
    {
        [SerializeField] private GameObject inventoryPrefab;
        [SerializeField] private int X_START;
        [SerializeField] private int Y_START;
        [SerializeField] private int X_SPASE_BETWEEN_ITEMS;
        [SerializeField] private int Y_SPASE_BETWEEN_ITEMS;
        
        [SerializeField] private GameObject _inventoryGO;
        [SerializeField] private List<ItemObject> items = new List<ItemObject>();

        private Dictionary<float, GameObject> _slotOnInteface = new Dictionary<float, GameObject>();
        
        private void OnEnable()
        {
            GameEvents.OnUpdateActiveMenuSlot += UpdateActiveMenuSlot;
        }
        
        private void OnDisable()
        {
            GameEvents.OnUpdateActiveMenuSlot -= UpdateActiveMenuSlot;
        }

        private void UpdateActiveMenuSlot(float activeMenuSlot)
        {
            GameObject obj = _slotOnInteface[activeMenuSlot];
            Image img1 = obj.transform.GetComponent<Image>();
            img1.color = new Color(0.34f, 0.23f, 0.17f, 1f);
        }

        private void Start()
        {
           CreateSlot();
        }
        
        public void CreateSlot()
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                
                Image img = obj.transform.GetChild(0).GetComponentInChildren<Image>();
                if (items.Count > 0 && items.Count - 1 >= i)
                {
                    img.sprite = items[i].uiDisplay;

                    Image img1 = obj.transform.GetComponent<Image>();
                    img1.color = Color.red;
                    
                    _slotOnInteface.Add(items[i].activeMenuSlot, obj);
                }
                else
                {
                    img.color = new Color(0.34f, 0.23f, 0.17f, 1f);
                }

                TextMeshProUGUI text = obj.GetComponentInChildren<TextMeshProUGUI>();
                text.text = "";
            }
        }
        private Vector3 GetPosition(int i)
        {
            return new Vector3(
                X_START + (X_SPASE_BETWEEN_ITEMS * (i % 5)),
                Y_START - (Y_SPASE_BETWEEN_ITEMS * (i / 5)),
                0
            );
        }
    }
}