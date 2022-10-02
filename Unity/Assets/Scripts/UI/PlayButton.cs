using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LD51.Unity
{
    public class PlayButton : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                SceneManager.LoadScene("SampleScene");
            });
        }
    }
}
