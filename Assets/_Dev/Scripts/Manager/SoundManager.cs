using UnityEngine;

public class SoundManager : MonoBehaviour, IAudioControl
{
    [field: SerializeField] public AudioSource AudioSource { get; private set; }
    [field: SerializeField] public AudioClip AudioClip { get; private set; }

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