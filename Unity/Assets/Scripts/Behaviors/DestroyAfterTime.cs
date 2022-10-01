using UnityEngine;

namespace Behaviors
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