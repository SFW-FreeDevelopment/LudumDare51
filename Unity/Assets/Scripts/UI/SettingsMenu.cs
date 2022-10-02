using LD51.Unity.Managers;
using LD51.Unity.Models;
using UnityEngine;
using UnityEngine.UI;

namespace LD51.Unity.UI
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private Toggle _screenShakeToggle;

        private static Settings Settings => SettingsManager.Instance.Settings;
        
        private void Start()
        {
            _screenShakeToggle.onValueChanged.AddListener(value =>
            {
                Settings.UseScreenShake = value;
                // TODO: Save
            });
        }
    }
}