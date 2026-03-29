using damage;
using UnityEngine;

namespace building
{
    public class BuildingComponent : Damage
    {
        [SerializeField] Material _buildingEnable;
        [SerializeField] Material _buildingDisable;
    
        bool _triggerEntered;
        bool _finishCreateBuilding;

        Material _objMaterial;
    
        private void Awake()
        {
            _objMaterial = GetComponent<Renderer>().material;
            GetComponent<Renderer>().material = _buildingEnable;
        }

        public bool GetBuildingEnable()
        {
            return !_triggerEntered;
        }

        public void UpdateMaterialBuilding()
        {
            GetComponent<Renderer>().material = _objMaterial;
            _finishCreateBuilding = true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (!_finishCreateBuilding)
            {
                GetComponent<Renderer>().material = _buildingDisable;
                _triggerEntered = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!_finishCreateBuilding)
            {
                GetComponent<Renderer>().material = _buildingEnable;
                _triggerEntered = false;
            }
        }
        
        private void Update()
        {
            if (_weight <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
