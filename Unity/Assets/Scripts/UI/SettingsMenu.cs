using System;
using LD51.Unity.Managers;
using LD51.Unity.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
// ReSharper disable Unity.NoNullPropagation

namespace LD51.Unity.UI
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private Toggle _screenShakeToggle;
        [SerializeField] private TMP_InputField _colorInputField;
        [SerializeField] private Button _leftCrosshairButton, _rightCrosshairButton;
        [SerializeField] private Image _crosshairImage;
        private int _currentCrosshairIndex = 0;
        private static Sprite[] Crosshairs => SettingsManager.Instance?.Crosshairs ?? Array.Empty<Sprite>();
        
        private static Settings Settings => SettingsManager.Instance?.Settings ?? new Settings();
        private static void Save()
        {
            SettingsManager.Instance.Save();
        }

        private void Start()
        {
            _screenShakeToggle.onValueChanged.AddListener(value =>
            {
                Settings.UseScreenShake = value;
                Save();
            });
            _colorInputField.onValueChanged.AddListener(value =>
            {
                _colorInputField.image.color = ColorUtility.TryParseHtmlString(value, out var color)
                    ? color
                    : Color.white;

                _crosshairImage.color = _colorInputField.image.color;
                
                Settings.ReticleColor = value;
                Save();
            });
            _colorInputField.onSubmit.AddListener(value =>
            {
                Settings.ReticleColor = value;
                Save();
            });
            _leftCrosshairButton.onClick.AddListener(() =>
            {
                _currentCrosshairIndex = _currentCrosshairIndex == 0
                    ? Crosshairs.Length - 1
                    : _currentCrosshairIndex - 1;

                _crosshairImage.sprite = Crosshairs[_currentCrosshairIndex];
                
                Settings.CrosshairIndex = _currentCrosshairIndex;
                Save();
            });
            _rightCrosshairButton.onClick.AddListener(() =>
            {
                _currentCrosshairIndex = _currentCrosshairIndex == Crosshairs.Length - 1
                    ? 0
                    : _currentCrosshairIndex + 1;

                _crosshairImage.sprite = Crosshairs[_currentCrosshairIndex];
                
                Settings.CrosshairIndex = _currentCrosshairIndex;
                Save();
            });
        }

        private void OnEnable()
        {
            _screenShakeToggle.isOn = Settings.UseScreenShake;
            _colorInputField.text = Settings.ReticleColor;
            _colorInputField.image.color = ColorUtility.TryParseHtmlString(Settings.ReticleColor, out var color)
                ? color
                : Color.white;
            _currentCrosshairIndex = Settings.CrosshairIndex;
            _crosshairImage.sprite = Crosshairs[_currentCrosshairIndex];
            _crosshairImage.color = _colorInputField.image.color;
        }
    }
}