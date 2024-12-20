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
        GameObject soundManagerGameObject = new GameObject(nameof(SoundManager));
        soundManagerGameObject.SetActive(false);

        SoundManager soundManager = soundManagerGameObject.AddComponent<SoundManager>();
        soundManager.AudioSource = soundManagerGameObject.AddComponent<AudioSource>();
        soundManager.AudioSource.playOnAwake = false;
        soundManager.AudioClip = Resources.Load<AudioClip>(bgmPath);

        soundManagerGameObject.SetActive(true);

        // Act
        IAudioControl audioControl = soundManager;
        audioControl.Play();

        // Assert
        Assert.IsTrue(soundManager.AudioSource.isPlaying, "AudioSource should be playing after Play() is called.");
    }

    [Test]
    public void Stop()
    {
        // Arrange
        GameObject soundManagerGameObject = new GameObject("SoundManager");
        soundManagerGameObject.SetActive(false);

        SoundManager soundManager = soundManagerGameObject.AddComponent<SoundManager>();
        soundManager.AudioSource = soundManagerGameObject.AddComponent<AudioSource>();
        soundManager.AudioClip = Resources.Load<AudioClip>("Sounds/Music/bgm_festival");

        soundManagerGameObject.SetActive(true);

        // Act
        IAudioControl audioControl = soundManager;
        audioControl.Play();
        audioControl.Stop();

        // Assert
        Assert.IsNotNull(soundManager.AudioSource, "AudioClip should not be null. ");
        Assert.IsFalse(soundManager.AudioSource.isPlaying);
    }
}
