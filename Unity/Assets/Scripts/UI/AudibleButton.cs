using LD51.Unity.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace LD51.Unity.UI
{
    [RequireComponent(typeof(Button))]
    public class AudibleButton : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                AudioManager.Instance.Play("click");
            });
        }
    }
}