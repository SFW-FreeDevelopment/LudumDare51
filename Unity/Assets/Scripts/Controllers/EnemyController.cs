using System;
using LD51.Unity.Models;
using UnityEngine;

namespace LD51.Unity.Controllers
{
    public class EnemyController : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private Transform _playerTransform;
        private float moveLimiter = 0.7f;
        
        public short CurrentHealth { get; private set; }

        [SerializeField] private Enemy _enemy;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            CurrentHealth = (short)_enemy.Health;
            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
        private void FixedUpdate()
        {
            var movementVector = (_playerTransform.position - transform.position).normalized;
            if (movementVector.x != 0 && movementVector.y != 0)
            {
                movementVector.x *= moveLimiter;
                movementVector.y *= moveLimiter;
            } 
            _rigidbody2D.velocity = new Vector2(movementVector.x * _enemy.Speed, movementVector.y * _enemy.Speed);
            
            Vector2.MoveTowards(transform.position, _playerTransform.position, Single.Epsilon);
        }

        public void OnTakeDamage()
        {
            CurrentHealth -= 10;
            if (CurrentHealth <= 0)
            {
                // TODO: Enemy dies
                Destroy(gameObject);
            }
        }
    }
}