using LD51.Unity.Controllers;
using UnityEngine;

namespace LD51.Unity.Behaviors
{
    public class Food : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                GameController.Instance.GainLife(10);
                Destroy(gameObject);
            }
        }
    }
}