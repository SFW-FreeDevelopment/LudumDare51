﻿using LD51.Unity.Controllers;
using Unity.Mathematics;
using UnityEngine;

namespace LD51.Unity.Behaviors
{
    public class Bullet : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                Instantiate(GameController.Instance.BloodSplatterPrefab, col.gameObject.transform.position, Quaternion.identity);
                var movementVector = (col.gameObject.transform.position - gameObject.transform.position).normalized;
                col.attachedRigidbody.AddForce(movementVector * 5000);
                col.gameObject.GetComponent<EnemyController>().OnTakeDamage();
                Destroy(gameObject);
            }
        }
    }
}