using UnityEngine;

namespace LD51.Unity.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }

        public GameObject Reticle;
        public SpriteRenderer SpriteRenderer { get; private set; }
        [Header("Prefabs")]
        public GameObject BulletPrefab;
        
        private const float IntervalBetweenShots = .25f;
        private float _nextShotAvailableAt = 0;
        private bool NextShotAvailable => Time.time >= _nextShotAvailableAt;

        [SerializeField] private float _movementForce = 5.0f;
        private Rigidbody2D _rigidbody2D;
        private float horizontal, vertical;
        private float moveLimiter = 0.7f;
        
        private void Awake()
        {
            Instance = this;
            _rigidbody2D = GetComponent<Rigidbody2D>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        private void Update()
        {
            horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
            vertical = Input.GetAxisRaw("Vertical"); // -1 is down
            
            if (Input.GetMouseButton(0) && NextShotAvailable)
                Shoot();
            
            var position = transform.position;
            transform.position = new Vector2(
                Mathf.Clamp(position.x, -29, 29),
                Mathf.Clamp(position.y, -29, 29)
            );
            
            if (SpriteRenderer.flipX && horizontal > 0)
                SpriteRenderer.flipX = false;
            else if (!SpriteRenderer.flipX && horizontal < 0)
                SpriteRenderer.flipX = true;
        }

        private void Shoot()
        {
            _nextShotAvailableAt = Time.time + IntervalBetweenShots;

            CinemachineShake.Instance.ShakeCamera(5f, .1f);
                
            var bulletVector = (Reticle.transform.position - transform.position).normalized * 25;
            Instantiate(BulletPrefab, transform.position, Quaternion.identity)
                .GetComponent<Rigidbody2D>().AddForce(bulletVector, ForceMode2D.Impulse);  
        }

        private void FixedUpdate()
        {
            if (horizontal != 0 && vertical != 0) // Check for diagonal movement
            {
                // limit movement speed diagonally, so you move at 70% speed
                horizontal *= moveLimiter;
                vertical *= moveLimiter;
            } 

            _rigidbody2D.velocity = new Vector2(horizontal * _movementForce, vertical * _movementForce);
        }

        public void Die()
        {
            Destroy(gameObject);
        }
    }
}
