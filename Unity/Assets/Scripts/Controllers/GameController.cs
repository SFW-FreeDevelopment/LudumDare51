using System;
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

        [SerializeField] private GameObject _spawnPointParent;
        private Transform[] _spawnPoints = Array.Empty<Transform>();
        [SerializeField] private Spawner[] _spawners;
        [SerializeField] private Slider _healthBar;
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private TextMeshProUGUI _finalScoreText;
        [Header("Prefabs")]
        [SerializeField] private GameObject _bloodSplatterPrefab;
        public GameObject BloodSplatterPrefab => _bloodSplatterPrefab;
        [SerializeField] private GameObject[] _enemyPrefabs;
        [SerializeField] private GameObject[] _foodPrefabs;
        
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

            _spawnPoints = _spawnPointParent.GetComponentsInChildren<Transform>().Except(new [] { transform }).ToArray();

            StartCoroutine(TimerRoutine());
            StartCoroutine(SpawnMonsterRoutine());
            StartCoroutine(SpawnFoodRoutine());
        }

        public void GainLife(int health)
        {
            PlayerHealth += health;
            if (PlayerHealth > 100) PlayerHealth = 100;
            _healthBar.value = PlayerHealth / 100f;
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
            PlayerController.Instance.Die();
            
            _finalScoreText.text = $"<b>Score:</b> {Score}";
            _gameOverPanel.SetActive(true);
            PlayerManager.Instance.Save(CurrentWave, Score);
        }

        private IEnumerator SpawnFoodRoutine()
        {
            while (true)
            {
                Spawn();
                yield return new WaitForSeconds(TimeBetweenWaves);
            }

            void Spawn()
            {
                var spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
                var food = _foodPrefabs[Random.Range(0, _foodPrefabs.Length)];
                Instantiate(food, spawnPoint.position, quaternion.identity);
            }
        }
        
        private IEnumerator SpawnMonsterRoutine()
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