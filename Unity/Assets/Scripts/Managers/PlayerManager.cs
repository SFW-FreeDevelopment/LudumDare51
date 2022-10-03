using LD51.Unity.Models;
using LD51.Unity.Services;
using Newtonsoft.Json;
using UnityEngine;

namespace LD51.Unity.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { get; private set; }
        public Player Player { get; private set; } = new();

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
        
        public void Save()
        {
            var json = JsonConvert.SerializeObject(Player);
            PlayerPrefs.SetString("PlayerData", json);
            PlayerService.Create(Player);
        }

        public void Save(int waves, int score)
        {
            var json = JsonConvert.SerializeObject(Player);
            PlayerPrefs.SetString("PlayerData", json);
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
            if (PlayerPrefs.HasKey("PlayerData"))
            {
                var json = PlayerPrefs.GetString("PlayerData");
                Debug.Log(json);
                try
                {
                    Player = JsonConvert.DeserializeObject<Player>(json) ?? new Player();
                    Debug.Log(Player?.Id ?? "no id");
                    PlayerService.Fetch(Player?.Id, player =>
                    {
                        Player = player;
                    });

                    if (Player.DisplayName == null)
                    {
                        Player.DisplayName = $"Player {Random.Range(1, 100000)}";
                        Save();
                    }
                }
                catch
                {
                    Player = new Player();
                    Player.DisplayName = $"Player {Random.Range(1, 100000)}";
                    Save();
                }
            }
            else
            {
                Player = new Player();
                Player.DisplayName = $"Player {Random.Range(1, 100000)}";
                Save();
            }
        }
    }
}