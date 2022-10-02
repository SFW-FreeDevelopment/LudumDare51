using System.Collections;
using System.Linq;
using LD51.Unity.Scene;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LD51.Unity.Controllers
{
    public class GameController : MonoBehaviour
    {
        private const float TimeBetweenWaves = 10;

        private Spawner _lastSpawner = null;
        
        [SerializeField] private Spawner[] _spawners;
        [Header("Prefabs")]
        [SerializeField] private GameObject _bloodSplatterPrefab;
        public GameObject BloodSplatterPrefab => _bloodSplatterPrefab;
        [SerializeField] private GameObject[] _enemyPrefabs;
        
        public static GameController Instance { get; private set; }
        public int TimeElapsed { get; private set; } = 0;
        public bool IsGameOver { get; private set; } = false;
        
        private void Awake()
        {
            Instance = this;
        }
        
        private void Start()
        {
            StartCoroutine(TimerRoutine());
            StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            while (true)
            {
                Spawn();
                for (var i = 0; i < TimeElapsed / TimeBetweenWaves; i++)
                {
                    Spawn();
                }
                yield return new WaitForSeconds(TimeBetweenWaves);
            }

            void Spawn()
            {
                var spawners = _lastSpawner != null
                    ? _spawners.Except(new [] { _lastSpawner }).ToArray()
                    : _spawners;

                var spawner = spawners[Random.Range(0, spawners.Length)];
                var enemy = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)];

                Instantiate(enemy, spawner.transform.position, quaternion.identity);
                _lastSpawner = spawner;
            }
        }

        private IEnumerator TimerRoutine()
        {
            while (!IsGameOver)
            {
                yield return new WaitForSeconds(1);
                TimeElapsed++;
            }
        }
    }
}