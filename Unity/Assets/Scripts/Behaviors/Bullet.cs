using LD51.Unity.Controllers;
using UnityEngine;

namespace LD51.Unity.Behaviors
{
    public class Bullet : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log("Trigger enter");
            Debug.Log($"{col.gameObject.name} - {col.gameObject.tag}");
            if (col.gameObject.CompareTag("Enemy"))
                col.gameObject.GetComponent<EnemyController>().OnTakeDamage();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            Debug.Log("Collision enter");
            if (col.gameObject.CompareTag("Enemy"))
                col.gameObject.GetComponent<EnemyController>().OnTakeDamage();
        }
    }
}