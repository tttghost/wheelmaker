using UnityEngine;
using WheelMaker.Manager.Interfaces;

namespace WheelMaker.Manager.Sounds
{
    public class SoundManager : MonoBehaviour, IAudioControl
    {
        [field: SerializeField] public AudioSource AudioSource { get; set; }
        [field: SerializeField] public AudioClip AudioClip { get; set; }

        private void Awake()
        {
            AudioSource.clip = AudioClip;
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
    }
}