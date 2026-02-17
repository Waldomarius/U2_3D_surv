using System;
using UnityEngine;

namespace player
{
    public class GroundChecker : MonoBehaviour
    {
        public Action<bool> OnGroundStateChange;
        private bool isGrounded;
    
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _raycastDistance = 0.6f;

        private void Update()
        {
            CheckGround();
        }

        private void CheckGround()
        {
            bool hit = Physics.Raycast(transform.position, Vector3.down, _raycastDistance, _groundLayer);
            if (hit != isGrounded)
            {
                isGrounded = hit;
                // Оповестим всех слушателей об изменении состояния земли
                OnGroundStateChange?.Invoke(isGrounded);
            }
        
            Debug.DrawRay(transform.position, Vector3.down * _raycastDistance, isGrounded ? Color.green : Color.red);
        }
    }
}
