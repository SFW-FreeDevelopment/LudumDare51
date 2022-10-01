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

        [SerializeField] private float _movementForce = 10.0f;
        private Rigidbody2D _rigidbody2D;
        private bool _isMoving = false;
        
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        
        private void Update()
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                _rigidbody2D.AddForce(Vector2.left * Time.deltaTime * _movementForce, ForceMode2D.Impulse);
                _isMoving = true;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                _rigidbody2D.AddForce(Vector2.right * Time.deltaTime * _movementForce, ForceMode2D.Impulse);
                _isMoving = true;
            }
            else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                _rigidbody2D.AddForce(Vector2.up * Time.deltaTime * _movementForce, ForceMode2D.Impulse);
                _isMoving = true;
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                _rigidbody2D.AddForce(Vector2.down * Time.deltaTime * _movementForce, ForceMode2D.Impulse);
                _isMoving = true;
            }
            else
            {
                _isMoving = false;
            }
            
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
