using System;
using LD51.Unity.Models;
using LD51.Unity.Services;
using Newtonsoft.Json;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LD51.Unity.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { get; private set; }
        public Player Player { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                Load();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Save()
        {
            var json = JsonConvert.SerializeObject(Player);
            PlayerPrefs.SetString("PlayerData", json);
            PlayerService.Create(Player);
        }

        public void Save(int waves, int score)
        {
            if (Player == null)
            {
                Player = new Player
                {
                    DisplayName = $"Player {Random.Range(1, 100000)}",
                };
                Save();
            }
            
            var json = JsonConvert.SerializeObject(Player);
            PlayerPrefs.SetString("PlayerData", json);
            Debug.Log($"Wave: {waves}{Environment.NewLine}Score: {score}");
            PlayerService.ProcessGameResults(Player.Id, new GameResults
            {
                Waves = waves,
                Score = score
            }, player =>
            {
                Player = player;
            });
        }

        private void Load()
        {
            Player = ReadFromPlayerPrefs();
            Save();
        }

        private Player ReadFromPlayerPrefs()
        {
            if (!PlayerPrefs.HasKey("PlayerData"))
            {
                return new Player
                {
                    DisplayName = $"Player {Random.Range(1, 100000)}"
                };
            }
            
            var json = PlayerPrefs.GetString("PlayerData");
            try
            {
                Player = JsonConvert.DeserializeObject<Player>(json) ?? new Player
                {
                    DisplayName = $"Player {Random.Range(1, 100000)}"
                };
                Player.Id ??= Guid.NewGuid().ToString();
                return Player;
            }
            catch
            {
                return new Player
                {
                    DisplayName = $"Player {Random.Range(1, 100000)}"
                };
            }
        }
    }
}