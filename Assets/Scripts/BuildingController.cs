using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingController : MonoBehaviour
{
    [SerializeField] float  _maxDistance = 20;
    [SerializeField] float  _rotate = 45;
    
    [SerializeField] private GameObject _inputController;
    [SerializeField] private GameObject _buildingPrefab;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _spawnBuilding;
    [SerializeField] private Material _buildingMaterial;

    [SerializeField] private LayerMask _groundLayer;

    private bool _isBuilding;
    private GameObject _obj;
    private Material _prefabMaterial;
    
    private PlayerInput _playerInput;
    private InputAction _rotateBuildAction;
    private InputAction _mouseButtonLeftAction;

    private float _width;
    private float _height;
    
    void Awake()
    {
        _playerInput = _inputController.GetComponent<PlayerInput>();
        _rotateBuildAction = _playerInput.actions["Rotate"];
        _mouseButtonLeftAction = _playerInput.actions["Mouse_0"];

        _width = _camera.pixelWidth / 2;
        _height = _camera.pixelHeight / 2;
    }

    private void OnEnable()
    {
        _rotateBuildAction.Enable();
        _mouseButtonLeftAction.Enable();
        _rotateBuildAction.performed += OnRotateBuildPerformed;
        _mouseButtonLeftAction.performed += OnButtonLeftdPerformed;
    }

    private void OnDisable()
    {
        _rotateBuildAction.Disable();
        _mouseButtonLeftAction.Disable();
        _rotateBuildAction.performed -= OnRotateBuildPerformed;
        _mouseButtonLeftAction.performed -= OnButtonLeftdPerformed;
    }

    private void OnRotateBuildPerformed(InputAction.CallbackContext obj)
    {
        // Вращение объекта вокруг своей оси
        _obj.transform.rotation = Quaternion.Euler(0, _obj.transform.eulerAngles.y + _rotate, 0);
    }

    private void OnButtonLeftdPerformed(InputAction.CallbackContext obj)
    {
        if (_obj)
        {
            _obj.GetComponent<Renderer>().material = _prefabMaterial;
            _obj = null;
        }
    }
    
    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            CreateBuilding();
        }

        if (_obj)
        {
            Vector3 point = new Vector3(_width, _height, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, _maxDistance, _groundLayer))
            {
                _obj.transform.position = hit.point;
            }
        }
    }

    private void CreateBuilding()
    {
        Vector3 point = new Vector3(_width, _height, 0);
        Ray ray = _camera.ScreenPointToRay(point);
        
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _maxDistance, _groundLayer))
        {
            _obj = Instantiate(
                _buildingPrefab,
                hit.point,  
                Quaternion.Euler(_buildingPrefab.transform.eulerAngles));
                
            // Устанавливаем родителя для текущего объекта
            _obj.transform.SetParent(_inputController.transform);
                
            _prefabMaterial = _obj.GetComponent<Renderer>().material;
            _obj.GetComponent<Renderer>().material = _buildingMaterial;
        }
    }
}