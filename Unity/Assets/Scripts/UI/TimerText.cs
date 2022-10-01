using System.Collections;
using LD51.Unity.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace LD51.Unity.UI
{
    [RequireComponent(typeof(Text))]
    public class TimerText : MonoBehaviour
    {
        private Text _text;
        
        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        private void Start()
        {
            StartCoroutine(TextUpdateRoutine());
        }

        private IEnumerator TextUpdateRoutine()
        {
            while (true)
            {
                _text.text = $"<b>Time:</b> {GameController.Instance.TimeElapsed}";
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}