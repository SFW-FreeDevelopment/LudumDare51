using Cinemachine;
using LD51.Unity.Managers;
using UnityEngine;

namespace LD51.Unity
{
    public class CinemachineShake : MonoBehaviour
    {
        public static CinemachineShake Instance { get; private set; }
        
        private CinemachineVirtualCamera cinemachineVirtualCamera;
        private float shakeTimerTotal;
        private float shakeTimer;
        private float startingIntensity;

        private void Awake()
        {
            Instance = this;
            cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        }


        public void ShakeCamera(float intensity, float time)
        {
            if (!SettingsManager.Instance.Settings.UseScreenShake) return;
            
            var cinemachineBasicMultiChannelPerlin =
                cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

            startingIntensity = intensity;
            shakeTimerTotal = time;
            shakeTimer = time;
        }

        private void Update()
        {
            if (shakeTimer > 0)
            {
                shakeTimer -= Time.deltaTime;
                if (shakeTimer <= 0f)
                {
                    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
                    Mathf.Lerp(startingIntensity, 0f, shakeTimer / shakeTimerTotal);
                }
            }
        }
    }
}
