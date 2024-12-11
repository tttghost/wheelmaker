using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour, IAudioControl
{
    [field: SerializeField] public AudioSource audioSource { get; set; }
    [field: SerializeField] public AudioClip audioClip { get; set; }

    private void Awake()
    {
        audioSource.clip = audioClip;
    }

    public void Play()
    {
        Debug.Log("Play");
        audioSource.Play();
    }

    public void Stop()
    {
        Debug.Log("Stop");
        audioSource.Stop();
    }

    #region legacy code
    /*
    private Dictionary<eAudioMixerType, AudioSource> audioSourceDics = new Dictionary<eAudioMixerType, AudioSource>();
    private Dictionary<eAudioClips, AudioClip> audioClipDics = new Dictionary<eAudioClips, AudioClip>();
    protected void Awake()
    {
        InitAudioSource();
        InitAudioClip();
    }

    /// <summary>
    /// 오디오소스 초기화
    /// </summary>
    private void InitAudioSource()
    {
        AudioMixer audioMixerController = Resources.Load<AudioMixer>(define.path_sound_audiomixer + nameof(audioMixerController));

        AudioMixerGroup[] audioMixerGroup = audioMixerController.FindMatchingGroups(eAudioMixerType.Master.ToString());
        for (int i = 0; i < audioMixerGroup.Length; i++)
        {
            AudioMixerGroup audioMixer = audioMixerGroup[i];

            GameObject go = new GameObject(audioMixerGroup[i].name);
            go.transform.SetParent(transform);

            AudioSource audioSource = go.AddComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = audioMixerGroup[i];

            audioSourceDics.Add(Util.String2Enum<eAudioMixerType>(audioSource.name), audioSource);
        }
    }

    /// <summary>
    /// 오디오클립 초기화
    /// </summary>
    private void InitAudioClip()
    {
        CachingAudioClip(define.path_sound_ambience);
        CachingAudioClip(define.path_sound_dialogue);
        CachingAudioClip(define.path_sound_interface);
        CachingAudioClip(define.path_sound_music);
        CachingAudioClip(define.path_sound_soundeffects);
    }

    /// <summary>
    /// 오디오클립 캐싱
    /// </summary>
    /// <param name="audioClipPath"></param>
    private void CachingAudioClip(string audioClipPath)
    {
        var audioClips = Resources.LoadAll<AudioClip>(audioClipPath);
        foreach (var audioClip in audioClips)
        {
            audioClipDics.Add(Util.String2Enum<eAudioClips>(audioClip.name), audioClip);
        }
    }


    /// <summary>
    /// BGM 재생
    /// </summary>
    /// <param name="eAudioClip"></param>
    /// <param name="volume"></param>
    public void PlayBGM(eAudioClips eAudioClip, float volume = 1f, bool loop = true)
    {
        if (audioClipDics.ContainsKey(eAudioClip))
        {
            AudioSource audioSource = audioSourceDics[eAudioMixerType.Music];
            audioSource.Stop();
            audioSource.clip = audioClipDics[eAudioClip];
            audioSource.volume = volume;
            audioSource.loop = loop;
            audioSource.Play();
        }
        else
        {
            Debug.LogError($"해당 {eAudioClip} audioClip이 존재하지 않음");
        }
    }

    public void StopBGM()
    {
        PauseBGM();
        audioSourceDics[eAudioMixerType.Music].clip = null;
    }
    public void PauseBGM()
    {
        audioSourceDics[eAudioMixerType.Music].Stop();
    }

    /// <summary>
    /// 이펙트 재생
    /// </summary>
    /// <param name="eAudioClip"></param>
    /// <param name="volumeScale"></param>
    public void PlayEffect(eAudioClips eAudioClip, float volumeScale = 1f)
    {
        if (audioClipDics.ContainsKey(eAudioClip))
        {
            audioSourceDics[eAudioMixerType.SoundEffects].PlayOneShot(audioClipDics[eAudioClip], volumeScale);
        }
        else
        {
            Debug.LogError($"해당 {eAudioClip} audioClip이 존재하지 않음");
        }
    }

    /// <summary>
    /// 음성 재생
    /// </summary>
    /// <param name="eAudioClip"></param>
    /// <param name="volumeScale"></param>
    public void PlayVoice(eAudioClips eAudioClip, float volumeScale = 1f)
    {
        if (audioClipDics.ContainsKey(eAudioClip))
        {
            AudioSource audioSource = audioSourceDics[eAudioMixerType.Interface];
            audioSource.Stop();
            audioSource.clip = audioClipDics[eAudioClip];
            audioSource.volume = volumeScale;
            audioSource.Play();
        }
        else
        {
            Debug.LogError($"해당 {eAudioClip} audioClip이 존재하지 않음");
        }
    }
    



public enum eAudioMixerType
{
    Master,
    Music,
    SoundEffects,
    Dialogue,
    Ambience,
    Interface,
}

public enum eAudioClips
{
    //ambience

    //bgm

    //effect
    effect_click,

    //interface

    //dialogue

    //voice
}
    */
    #endregion
}