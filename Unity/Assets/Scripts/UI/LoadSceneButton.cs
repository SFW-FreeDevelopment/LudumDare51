using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LD51.Unity
{
    public class LoadSceneButton : MonoBehaviour
    {
        [SerializeField] protected string _sceneName;
        
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                SceneManager.LoadScene(_sceneName);
            });
        }
    }
}
