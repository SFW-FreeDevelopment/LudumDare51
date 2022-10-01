using System.Collections;
using UnityEngine;

namespace LD51.Unity.Controllers
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }
        public int TimeElapsed { get; private set; } = 0;
        public bool IsGameOver { get; private set; } = false;
        
        private void Awake()
        {
            Instance = this;
        }
        
        private void Start()
        {
            StartCoroutine(TimerRoutine());
        }
        
        private IEnumerator TimerRoutine()
        {
            while (!IsGameOver)
            {
                yield return new WaitForSeconds(1);
                TimeElapsed++;
            }
        }
    }
}