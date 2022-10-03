using System;
using LD51.Unity.Managers;
using LD51.Unity.Models;
using Unity.Mathematics;
using UnityEngine;

namespace LD51.Unity.Controllers
{
    public class EnemyController : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private Transform _playerTransform;
        private SpriteRenderer _spriteRenderer;
        private float moveLimiter = 0.7f;
        
        public short CurrentHealth { get; private set; }

        [SerializeField] private Enemy _enemy;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            CurrentHealth = (short)_enemy.Health;
            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
        private void FixedUpdate()
        {
            if (GameController.Instance.IsGameOver) return;
            
            var movementVector = (_playerTransform.position - transform.position).normalized;
            if (movementVector.x != 0 && movementVector.y != 0)
            {
                movementVector.x *= moveLimiter;
                movementVector.y *= moveLimiter;
            } 
            _rigidbody2D.velocity = new Vector2(movementVector.x * _enemy.Speed, movementVector.y * _enemy.Speed);
            
            Vector2.MoveTowards(transform.position, _playerTransform.position, Single.Epsilon);

            if (_spriteRenderer.flipX && movementVector.x > 0)
                _spriteRenderer.flipX = false;
            else if (!_spriteRenderer.flipX && movementVector.x < 0)
                _spriteRenderer.flipX = true;
        }

        public void OnTakeDamage()
        {
            CurrentHealth -= 10;
            if (CurrentHealth <= 0)
            {
                GameController.Instance.Score += 10;
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                AudioManager.Instance.Play("hitHurt");
                CinemachineShake.Instance.ShakeCamera(5f, .1f);
                GameController.Instance.TakeDamage(_enemy.Damage);
                Instantiate(GameController.Instance.BloodSplatterPrefab, col.gameObject.transform.position, Quaternion.identity);
                var movementVector = (col.gameObject.transform.position - gameObject.transform.position).normalized;
                col.rigidbody.AddForce(movementVector * 5000);
            }
        }
    }
}