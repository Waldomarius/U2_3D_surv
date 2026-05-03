using System.Collections;
using damage;
using eventSystem;
using UnityEngine;

namespace enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] protected float _weight = 100f;
        
        private float _speed;
        private Animator _animator;
        private Rigidbody _rb;
        private Transform _playerTransform;
        private Vector3 _movement;
        
        private Vector3 _startPosition;
        private bool _isAttacking = false;
        private bool _addDamage = true;
        private bool _dead = false;
        private float _damage;
        private float _startWeight;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody>();
            
            _startWeight = _weight;
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
            if (!_dead)
            {
                // Поворот врага в сторону персонажа
                Vector3 rotation = new Vector3(
                    _playerTransform.position.x,
                    _playerTransform.position.y - 1,
                    _playerTransform.position.z);
                transform.LookAt(rotation);

                Vector3 direction = _playerTransform.position - transform.position;
                direction.Normalize();
                _movement = direction;

                _animator.SetBool("Attack", _isAttacking);
            }
        }

        private void FixedUpdate()
        {
            if (!_isAttacking && !_dead)
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
                else
                {
                    _isAttacking = false;
                    _addDamage = true;
                }
            }
        }
        
        private IEnumerator AddDamage()
        {
            yield return new WaitForSeconds(1f);
            _isAttacking = false;
            _addDamage = true;
        }

        public void SetStartPosition(Vector3 position)
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
        
        public void UpdateWeight(float value)
        {
            _weight -= value;

            if (_weight <= 0)
            {
                _dead = true;
                _animator.SetBool("Dead", true);
                
                StartCoroutine(RespawnEnemy());
                
                // Destroy(gameObject, 5);
            }
        }


        private IEnumerator RespawnEnemy()
        {
            yield return new WaitForSeconds(8f);

            gameObject.transform.position = _startPosition;
            _weight = _startWeight;
            
            
            _dead  = false;
            gameObject.SetActive(true);
            _isAttacking = false;
            _addDamage = true;
            _animator.SetBool("Reset", true);

        }

        public bool CheckAddDamage(float value)
        {
            float result = _weight - value;
            return result >= 0;
        }
    }
}