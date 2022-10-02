using LD51.Unity.Managers;
using UnityEngine;

namespace LD51.Unity
{
    public class CrosshairCursor : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Camera _camera;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _camera = Camera.main;
            #if !UNITY_EDITOR
            Cursor.visible = false;
            #endif
        }
        
        private void Start()
        {
            var colorString = SettingsManager.Instance.Settings.ReticleColor;
            _spriteRenderer.color = ColorUtility.TryParseHtmlString(colorString, out var color)
                ? color
                : Color.red;
            _spriteRenderer.sprite = SettingsManager.Instance.Crosshairs[SettingsManager.Instance.Settings.CrosshairIndex];
        }

        private void Update()
        {
            Vector2 mouseCursorPos = _camera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mouseCursorPos;
        }
    }
}
