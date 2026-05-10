using System;
using System.Collections;
using damage;
using eventSystem;
using scenes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace player
{
    public class PlayerController : Damage
    {
        private Vector2 _moveInput;
        private Vector2 _lookInput;
        private Rigidbody _rb;
        private float _rotationX;
        private bool _isGrounded;
        private bool _isOpenedUI;
        private GroundChecker _groundChecker;
        private float _rotationInOpenUI = Single.NaN;
        private float _startWeight;

        private PlayerInput _playerInput;
        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _jumpAction;


        [Header("Moving options")]
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] private float _sensitivity = 5f;
        [SerializeField] private float _maxAngle = 60f;
        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject _inputController;


        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _playerInput = _inputController.GetComponent<PlayerInput>();
            _groundChecker = GetComponent<GroundChecker>();
        
            _moveAction = _playerInput.actions["Moved"];
            _lookAction = _playerInput.actions["Look"];
            _jumpAction = _playerInput.actions["Jump"];

            _startWeight = _weight;
            GameEvents.PlayerPosition(transform);
        }
        private void Start()
        {
            GameEvents.UpdateHealthInfo(_weight);
        }

        private void OnEnable()
        {
            _moveAction.Enable();
            _jumpAction.Enable();
            _lookAction.Enable();
        
            _moveAction.performed += OnMovePerformed;
            _moveAction.canceled += OnMoveCanceled;
            _jumpAction.performed += OnJumpPerformed;
            _lookAction.performed += OnLookPerformed;
            _lookAction.canceled += OnLookCanceled;
        
            _groundChecker.OnGroundStateChange += HandleGroundStateChanged;
            
            GameEvents.OnOpenedUI += OpenedUI;
        }

        private void OpenedUI(bool isOpenedUI)
        {
            _isOpenedUI = isOpenedUI;
        }

        private void OnDisable()
        {
            _moveAction.Disable();
            _jumpAction.Disable();
            _lookAction.Disable();
        
            _moveAction.performed -= OnMovePerformed;
            _moveAction.canceled -= OnMoveCanceled;
            _jumpAction.performed -= OnJumpPerformed;
            _lookAction.performed -= OnLookPerformed;
            _lookAction.canceled -= OnLookCanceled;
        
            _groundChecker.OnGroundStateChange -= HandleGroundStateChanged;
            
            GameEvents.OnOpenedUI -= OpenedUI;
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();
        }

        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            _moveInput = Vector2.zero;
        }

        private void OnJumpPerformed(InputAction.CallbackContext context)
        {
            if (_isGrounded)
            {
                _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            }
        }

        private void OnLookPerformed(InputAction.CallbackContext context)
        {
            _lookInput = context.ReadValue<Vector2>();
        }

        private void OnLookCanceled(InputAction.CallbackContext context)
        {
            _lookInput = Vector2.zero;
        }

        private void HandleGroundStateChanged(bool grounded)
        {
            _isGrounded = grounded;
        }

        private void Update()
        {
            GameEvents.PlayerPosition(transform);
            
            if (_weight <= 0)
            {
                Scenes.Dead();
            }
        }

        private void FixedUpdate()
        {
            // Направляем движение игрока в сторону поворота камеры
            Vector3 move = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0) * new Vector3(_moveInput.x, 0, _moveInput.y) * (_moveSpeed * Time.fixedDeltaTime);
            Vector3 moveGlobal = transform.position + move;
            _rb.MovePosition(moveGlobal);

            // Поворачиваем игрока в сторону поворота камеры по оси y (влево/вправо)
            transform.rotation = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0);

            // При открытии UI надо заблокировать движение мышью
            if (_isOpenedUI)
            {
                if (float.IsNaN(_rotationInOpenUI))
                {
                    _rotationInOpenUI = _camera.transform.localEulerAngles.y;
                }
                _camera.transform.localEulerAngles = new Vector3(15, _rotationInOpenUI, 0);
            }
            else
            {
                // Поворачиваем камеру вверх/вниз
                _rotationX -= _lookInput.y * _sensitivity * Time.fixedDeltaTime;
                _rotationX = Mathf.Clamp(_rotationX, -_maxAngle, _maxAngle);
                float rotationY = _camera.transform.localEulerAngles.y;
                _camera.transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);

                // Поворачиваем камеру влево/вправо
                _camera.transform.Rotate(Vector3.up * (_lookInput.x * _sensitivity * Time.fixedDeltaTime));

                _rotationInOpenUI = Single.NaN;
            }

            // Двигаем камеру вслед за игроком на высоте 1
            _camera.transform.position = moveGlobal + new Vector3(0,1,0);
        }
        
        public override void UpdateWeight(float value)
        {
            _weight -= value;
            GameEvents.UpdateHealthInfo(_weight);
        }
        
        public void UpdateHealthWeight(float value)
        {
            _weight += value;

            if (_weight > _startWeight)
            {
                _weight = _startWeight;
            }
            
            GameEvents.UpdateHealthInfo(_weight);
        }

        public bool CanUpdateWeight()
        {
            return _weight < _startWeight;
        }
        
    }
}