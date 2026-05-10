using eventSystem;
using inventorySystem;
using Items.scritableObjects.items;
using player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace activeMenu
{
    public class AppleController : MonoBehaviour
    {
        [SerializeField] private GameObject _inputController;
        [SerializeField] private GameObject _playerControllerGO;
        [SerializeField] private GameObject _inventoryContainerGO;
        [SerializeField] private ItemObject _appleItem;
        [SerializeField] private float _weight = 5f;
        
        private PlayerInput _playerInput;
        private InputAction _appleAction;
        private PlayerController _playerController;
        private InventoryContainer _inventoryContainer;
        
        void Awake()
        {
            _playerController = _playerControllerGO.GetComponent<PlayerController>();
            _playerInput = _inputController.GetComponent<PlayerInput>();
            _inventoryContainer = _inventoryContainerGO.GetComponent<InventoryContainer>();
            _appleAction = _playerInput.actions["Health1"];
        }

        private void OnEnable()
        {
            _appleAction.Enable();
            _appleAction.performed += OnApplePerformed;
        }

        private void OnDisable()
        {
            _appleAction.Disable();
            _appleAction.performed -= OnApplePerformed;
            
        }

        private void OnApplePerformed(InputAction.CallbackContext obj)
        {
            if (_playerController.CanUpdateWeight())
            {
                Item itemFull = new Item(_appleItem);

                if (_inventoryContainer.FindItemOnInventorySlot(itemFull) != null)
                {
                    _inventoryContainer.RemoveItemCountFromStorage(itemFull, 1);
                    if (_playerController.CanUpdateWeight())
                    {
                        _playerController.UpdateHealthWeight(_weight);
                    }

                    // Обновим слоты в меню
                    _inventoryContainer.UpdateInventory();

                    // Если был последний на складе
                    if (_inventoryContainer.FindItemOnInventorySlot(itemFull) == null)
                    {
                        GameEvents.UpdateDissableActiveMenuSlot(itemFull.activeMenuSlot);
                    }
                }
                else
                {
                    GameEvents.UpdateDissableActiveMenuSlot(itemFull.activeMenuSlot);
                }
            }
        }
        

    }
}