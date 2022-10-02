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
        }

        private void Load()
        {
            if (PlayerPrefs.HasKey("PlayerData"))
            {
                var json = PlayerPrefs.GetString("PlayerData");
                try
                {
                    Player = JsonConvert.DeserializeObject<Player>(json) ?? new Player();
                    if (Player?.Id != null)
                    {
                        PlayerService.Fetch(Player.Id, player =>
                        {
                            Player = player;
                        });
                    }
                    else
                    {
                        Player = new Player();
                    }
                }
                catch
                {
                    Player = new Player();
                }
            }
            else
            {
                Player = new Player();
            }
        }
    }
}