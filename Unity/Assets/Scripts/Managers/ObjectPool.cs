using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LD51.Unity.Managers
{
    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool Instance { get; private set; }
        
        private Dictionary<string, List<GameObject>> _poolDictionary = new();
        
        private void Awake()
        {
            Instance = this;
        }

        public void Instantiate(GameObject obj)
        {
            var key = gameObject.name;
            if (_poolDictionary.ContainsKey(key))
            {
                var pool = _poolDictionary[key];
                var availableObj = pool.FirstOrDefault(x => !x.activeSelf);
                if (availableObj == null)
                {
                    pool.Add(GameObject.Instantiate(obj));
                }
                else
                {
                    availableObj.SetActive(true);
                    // TODO: Reset position
                }
            }
            else
            {
                _poolDictionary.Add(key, new List<GameObject>
                {
                    GameObject.Instantiate(obj)
                });
            }
        }
        
        public void Destroy(GameObject obj)
        {
            var key = gameObject.name;
            if (_poolDictionary.ContainsKey(key))
            {
                
            }
        }
    }
}