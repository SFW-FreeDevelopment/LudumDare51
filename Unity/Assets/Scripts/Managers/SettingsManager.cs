using LD51.Unity.Models;
using UnityEngine;

namespace LD51.Unity.Managers
{
    public class SettingsManager : MonoBehaviour
    {
        public static SettingsManager Instance { get; private set; }

        public Settings Settings { get; set; } = new();
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            // TODO: Load settings or initialize
        }
    }
}