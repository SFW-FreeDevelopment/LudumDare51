using System.Collections;
using System.Linq;
using LD51.Unity.Managers;
using LD51.Unity.Scene;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace LD51.Unity.Controllers
{
    public class GameController : MonoBehaviour
    {
        private const float TimeBetweenWaves = 10;

        private Spawner _lastSpawner = null;
        public int CurrentWave { get; private set; } = 1;
        public int PlayerHealth { get; private set; } = 100;
        public int Score { get; set; }
        
        [SerializeField] private Spawner[] _spawners;
        [SerializeField] private Slider _healthBar;
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private TextMeshProUGUI _finalScoreText;
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
            _healthBar.value = PlayerHealth / 100f;
            StartCoroutine(TimerRoutine());
            StartCoroutine(SpawnRoutine());
        }

        public void TakeDamage(int damage)
        {
            PlayerHealth -= damage;
            if (PlayerHealth <= 0)
            {
                PlayerHealth = 0;
                IsGameOver = true;
                GameOver();
            }
            _healthBar.value = PlayerHealth / 100f;
        }

        private void GameOver()
        {
            _finalScoreText.text = $"<b>Score:</b> {Score}";
            _gameOverPanel.SetActive(true);
            PlayerManager.Instance.Save(CurrentWave, Score);
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
                if (TimeElapsed % 10 == 0)
                    CurrentWave++;
            }
        }
    }
}