using eventSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace craft
{
    public class CraftController : MonoBehaviour
    {
        [SerializeField] private GameObject _inputController;

        private PlayerInput _playerInput;
        private InputAction _tabAction;
        private Canvas _canvas;
        
        void Awake()
        {
            _playerInput = _inputController.GetComponent<PlayerInput>();
            _tabAction = _playerInput.actions["Tab"];

            _canvas = GetComponentInChildren<Canvas>();
            _canvas.enabled = false;
        }

        private void OnEnable()
        {
            // Подписка на слушателя
            GameEvents.OnCloseUI += HandleCloseUI;
            
            _tabAction.Enable();
            _tabAction.performed += OnTabPerformed;
        }

        private void OnDisable()
        {
            // Отписка на слушателя
            GameEvents.OnCloseUI -= HandleCloseUI;
            
            _tabAction.Disable();
            _tabAction.performed -= OnTabPerformed;
        }
        
        private void HandleCloseUI(bool obj)
        {
            bool status = _canvas.enabled;
            _canvas.enabled = !status;
        }
        
        private void OnTabPerformed(InputAction.CallbackContext obj)
        {
            bool status = _canvas.enabled;
            _canvas.enabled = !status;
        }
    }
}