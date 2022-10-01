using UnityEngine;

namespace LD51.Unity.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        public GameObject Reticle;
        [Header("Prefabs")]
        public GameObject BulletPrefab;
        
        private const float IntervalBetweenShots = .25f;
        private float _nextShotAvailableAt = 0;
        private bool NextShotAvailable => Time.time >= _nextShotAvailableAt;

        private void Update()
        {
            if (Input.GetMouseButton(0) && NextShotAvailable)
            {
                _nextShotAvailableAt = Time.time + IntervalBetweenShots;

                var bulletVector = (Reticle.transform.position - transform.position).normalized * 25;
                Instantiate(BulletPrefab, transform.position, Quaternion.identity)
                    .GetComponent<Rigidbody2D>().AddForce(bulletVector, ForceMode2D.Impulse);
            }
        
        
            return;
            // TODO: Get current mouse position to determine target position
            Vector2 mouse = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
            Ray ray;
            ray = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
         
            if(Physics.Raycast(ray,out hit, 10))
            {
                Reticle.transform.position = hit.point;
            
                if(hit.point.x < transform.position.x)
                    Debug.Log("Left");
                else
                    Debug.Log("Right");
            }
        }
    }
}
