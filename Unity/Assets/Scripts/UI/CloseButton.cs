using UnityEngine;
using UnityEngine.UI;

namespace LD51.Unity.UI
{
    public class CloseButton : MonoBehaviour
    {
        [SerializeField] private GameObject _target;
        
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                _target.SetActive(false);
            });
        }
    }
}