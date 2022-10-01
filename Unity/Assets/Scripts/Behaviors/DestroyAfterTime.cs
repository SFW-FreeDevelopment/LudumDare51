using UnityEngine;

namespace LD51.Unity.Behaviors
{
    public class DestroyAfterTime : MonoBehaviour
    {
        public float Time = 3;
        
        public void Start()
        {
            Destroy(gameObject, Time);
        }
    }
}