using UnityEngine;
using WheelMaker.Manager.Interfaces;

namespace WheelMaker.Manager.Sounds
{
    public class SoundManager : MonoBehaviour, IAudioControl, IVolumeControl
    {
        [field: SerializeField] public AudioSource AudioSource { get; private set; }

        private float volume;
        public float Volume 
        { 
            get => volume;
            set 
            {
                volume = value;
                Debug.Log($"Set volume : {volume.ToString("0.00")}");
                if(AudioSource != null )
                {
                    AudioSource.volume = value;
                }
            } 
        }

        public void Initialize(AudioSource audioSource, AudioClip audioClip)
        {
            AudioSource = audioSource;
            if (audioSource != null)
            {
                audioSource.clip = audioClip;
            }
        }

        public void Play()
        {
            Debug.Log("Play");
            AudioSource.Play();
        }

        public void Stop()
        {
            Debug.Log("Stop");
            AudioSource.Stop();
        }

        public void Pause()
        {
            Debug.Log("Pause");
            AudioSource.Pause();
        }

        public void UnPause()
        {
            Debug.Log("UnPause");
            AudioSource.UnPause();
        }
    }
}