using LD51.Unity.Models;
using Newtonsoft.Json;
using UnityEngine;

namespace LD51.Unity.Managers
{
    public class SettingsManager : MonoBehaviour
    {
        public static SettingsManager Instance { get; private set; }
        public Settings Settings { get; private set; } = new();

        public Sprite[] Crosshairs;

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
            var json = JsonConvert.SerializeObject(Settings);
            PlayerPrefs.SetString("Settings", json);
        }

        private void Load()
        {
            if (PlayerPrefs.HasKey("Settings"))
            {
                var json = PlayerPrefs.GetString("Settings");
                try
                {
                    Settings = JsonConvert.DeserializeObject<Settings>(json) ?? new Settings();
                }
                catch
                {
                    Settings = new Settings();
                }
            }
            else
            {
                Settings = new Settings();
            }
        }
    }
}