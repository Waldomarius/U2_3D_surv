using System;
using System.Collections;
using building;
using damage;
using env;
using eventSystem;
using player;
using UnityEngine;

namespace enemy
{
    public class EnemyController : MonoBehaviour
    {
        private float _speed;
        private Animator _animator;
        private Rigidbody _rb;
        private Transform _playerTransform;
        private Vector3 _movement;
        
        private Vector2 _startPosition;
        private bool _isAttacking = false;
        private bool _addDamage = true;
        private float _damage;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody>();
        }
        
        private void OnEnable()
        {
            GameEvents.OnPlayerPosition += PlayerPosition;
        }
        
        private void OnDisable()
        {
            GameEvents.OnPlayerPosition -= PlayerPosition;
        }

        private void PlayerPosition(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }

        private void Update()
        {
            // Поворот врага в сторону персонажа
            Vector3 rotation = new Vector3(_playerTransform.position.x , _playerTransform.position.y - 1, _playerTransform.position.z);
            transform.LookAt(rotation);
            
            Vector3 direction = _playerTransform.position - transform.position;
            direction.Normalize();
            _movement = direction;
            
            _animator.SetBool("Attack", _isAttacking);
        }

        private void FixedUpdate()
        {
            if (!_isAttacking)
            {
                _rb.MovePosition(transform.position + _movement * (_speed * Time.deltaTime));
                _animator.SetFloat("MoveSpeed", 1);
            }

        }
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Tree")
                || other.CompareTag("Player")
                ||other.CompareTag("Building"))
            {
                AddDamage(other.gameObject);
            }
        }

        private void AddDamage(GameObject tree)
        {
            _isAttacking = true;

            if (_addDamage)
            {
                _addDamage =  false;
                Damage component = tree.GetComponent<Damage>();
                    
                if (component.CheckAddDamage(_damage))
                {
                    component.UpdateWeight(_damage);
                    StartCoroutine(AddDamage());
                }
            }
        }
        
        private IEnumerator AddDamage()
        {
            yield return new WaitForSeconds(1f);
            _isAttacking = false;
            _addDamage = true;
        }

        public void SetStartPosition(Vector2 position)
        {
            _startPosition = position;
        }

        public void SetMoveSpeed(float moveSpeed)
        {
            _speed = moveSpeed;
        }
        
        public void SetDamage(float damage)
        {
            _damage = damage;
        }
    }
}