using NUnit.Framework;
using UnityEngine;
using WheelMaker.Manager.Interfaces;
using WheelMaker.Manager.Sounds;

public class AudioControlTest
{
    static string[] testcaseBGMs = new string[] 
    {
        "Sounds/Music/bgm_festival",
        "Sounds/Music/a",
        "Sounds/Music/b",
    };

    [Test]
    public void Play([ValueSource(nameof(testcaseBGMs))] string bgmPath)
    {
        // Arrange
        var go = new GameObject(nameof(SoundManager));
        var soundManager = go.AddComponent<SoundManager>();
        var audioSource = go.AddComponent<AudioSource>();
        var audioClip = Resources.Load<AudioClip>(bgmPath);
        soundManager.Initialize(audioSource, audioClip);

        // Act
        IAudioControl audioControl = soundManager;
        audioControl.Stop();
        audioControl.Play();

        // Assert
        Assert.IsTrue(soundManager.AudioSource.isPlaying, "AudioSource should be playing after Play() is called.");
    }

    [Test]
    public void Stop()
    {
        // Arrange
        GameObject go = new GameObject(nameof(SoundManager));
        SoundManager soundManager = go.AddComponent<SoundManager>();
        var audioSource = go.AddComponent<AudioSource>();
        var audioClip = Resources.Load<AudioClip>("Sounds/Music/bgm_festival");
        soundManager.Initialize(audioSource, audioClip);
        soundManager.AudioSource.playOnAwake = false;

        // Act
        IAudioControl audioControl = soundManager;
        audioControl.Play();
        audioControl.Stop();

        // Assert
        Assert.IsNotNull(soundManager.AudioSource, "AudioClip should not be null. ");
        Assert.IsFalse(soundManager.AudioSource.isPlaying);
    }
}
