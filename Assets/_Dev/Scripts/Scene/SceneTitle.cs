using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using WheelMaker.Manager.Sounds;

public class SceneTitle : MonoBehaviour
{
    [field:SerializeField] private SoundManager soundManager;
    void Start()
    {
        InitializeSoundManager();
    }

    void InitializeSoundManager()
    {
        var audioSource = soundManager.GetComponent<AudioSource>();
        var audioClip = Resources.Load<AudioClip>("Sounds/Music/bgm_festival");
        soundManager.Initialize(audioSource, audioClip);
        soundManager.Play();
    }
}
