using UnityEngine;

namespace LD51.Unity
{
    public class CrosshairCursor : MonoBehaviour
    {
        private Camera _camera;
        
        private void Awake()
        {
            _camera = Camera.main;
            #if !UNITY_EDITOR
            Cursor.visible = false;
            #endif
        }

        private void Update()
        {
            Vector2 mouseCursorPos = _camera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mouseCursorPos;
        }
    }
}
