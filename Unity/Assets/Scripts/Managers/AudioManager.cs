using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LD51.Unity.Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [SerializeField] private GameObject _audioObjectPrefab;

        private readonly IDictionary<string, AudioClip> _audioClipDictionary = new Dictionary<string, AudioClip>();
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
            
            DontDestroyOnLoad(gameObject);

            var audioClips = Resources.LoadAll<AudioClip>("Audio");
            if (audioClips?.Any() ?? false)
            {
                foreach (var audioClip in audioClips)
                {
                    if (!_audioClipDictionary.TryGetValue(audioClip.name, out _))
                    {
                        _audioClipDictionary.Add(audioClip.name, audioClip);
                    }
                }
            }
        }

        public void Play(string clipName)
        {
            if (_audioClipDictionary.TryGetValue(clipName, out var clip))
            {
                var spawnedObj = Instantiate(_audioObjectPrefab);
                var audioSource = spawnedObj.GetComponent<AudioSource>();
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
    }
}