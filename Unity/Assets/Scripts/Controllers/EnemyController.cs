using System;
using UnityEngine;

namespace LD51.Unity.Controllers
{
    public class EnemyController : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private Transform _playerTransform;
        private int _moveSpeed = 5;
        private float moveLimiter = 0.7f;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
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
            _rigidbody2D.velocity = new Vector2(movementVector.x * _moveSpeed, movementVector.y * _moveSpeed);
            
            Vector2.MoveTowards(transform.position, _playerTransform.position, Single.Epsilon);
        }
    }
}