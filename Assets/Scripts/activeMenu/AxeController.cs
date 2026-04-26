using System.Collections;
using enemy;
using eventSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace activeMenu
{
    public class AxeController : MonoBehaviour
    {
        [SerializeField] private float _maxRoration = 45;
        [SerializeField] private float _diffRoration = 0.1f;
        [SerializeField] private float _damage = 50f;

        [SerializeField] private GameObject _inputController;
        
        private float _tempRotation = 0;
        private Camera _camera;
        private PlayerInput _playerInput;
        private InputAction _axeAction;
        private InputAction _mouseButtonLeftAction;
        
        private bool _isActive = false;
        private bool _isOpenedUI = false;
        private Quaternion _startOrientation;

        private bool _isAttacking = false;
        private bool _addDamage = true;

        void Awake()
        {
            _camera =  Camera.main;
            _playerInput = _inputController.GetComponent<PlayerInput>();
            _axeAction = _playerInput.actions["Axe"];
            _mouseButtonLeftAction = _playerInput.actions["Mouse_0"];
            transform.rotation = Quaternion.Euler(-_maxRoration, 0, 0);
        }

        private void OnEnable()
        {
            _axeAction.Enable();
            _mouseButtonLeftAction.Enable();

            _axeAction.performed += OnAxePerformed;
            _mouseButtonLeftAction.performed += OnButtonLeftdPerformed;
            
            GameEvents.OnOpenedUI += OpenedUI;
        }

        private void OnDisable()
        {
            _axeAction.Disable();
            _mouseButtonLeftAction.Disable();
            _axeAction.performed -= OnAxePerformed;
            _mouseButtonLeftAction.performed -= OnButtonLeftdPerformed;
            
            GameEvents.OnOpenedUI -= OpenedUI;
        }
        
        private void OpenedUI(bool isOpenedUI)
        {
            _isOpenedUI = isOpenedUI;
        }

        private void OnButtonLeftdPerformed(InputAction.CallbackContext obj)
        {
            if (_isActive && !_isOpenedUI)
            {
                StartCoroutine(AddDamage());
            }
        }

        private void OnAxePerformed(InputAction.CallbackContext obj)
        {
            if (_isActive)
            {
                transform.rotation = Quaternion.Euler(-_maxRoration, 0, 0);
                _isActive =  false;
            }
            else
            {
                transform.rotation = Quaternion.Euler(_camera.transform.eulerAngles.x, _camera.transform.eulerAngles.y, 0);
                _isActive = true;
            }
        }

        private IEnumerator AddDamage()
        {
            transform.rotation = Quaternion.Euler(_camera.transform.eulerAngles.x + _tempRotation, _camera.transform.eulerAngles.y, 0);
            _tempRotation += _diffRoration;
            yield return new WaitForSeconds(0.01f);

            if (_tempRotation < _maxRoration)
            {
                StartCoroutine(AddDamage());
            }
            else
            {
                _tempRotation = 0;
                transform.rotation = Quaternion.Euler(_camera.transform.eulerAngles.x, _camera.transform.eulerAngles.y, 0);
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                AddDamageEnemy(other.gameObject);
            }
        }
        
        private void AddDamageEnemy(GameObject tree)
        {
            _isAttacking = true;

            if (_addDamage)
            {
                _addDamage =  false;
                EnemyController component = tree.GetComponent<EnemyController>();
                    
                if (component.CheckAddDamage(_damage))
                {
                    component.UpdateWeight(_damage);
                    StartCoroutine(AddDamageEnemy());
                }
            }
        }
        
        private IEnumerator AddDamageEnemy()
        {
            yield return new WaitForSeconds(1f);
            _isAttacking = false;
            _addDamage = true;
        }
    }
}
