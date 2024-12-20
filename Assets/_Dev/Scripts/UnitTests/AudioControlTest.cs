using NUnit.Framework;
using UnityEngine;
using WheelMaker.Manager.Interfaces;
using WheelMaker.Manager.Sounds;

public class AudioControlTest
{
    [Test]
    public void Play()
    {
        // Arrange
        GameObject soundManagerGameObject = new GameObject("SoundManager");
        soundManagerGameObject.SetActive(false);

        SoundManager soundManager = soundManagerGameObject.AddComponent<SoundManager>();
        soundManager.AudioSource = soundManagerGameObject.AddComponent<AudioSource>();
        soundManager.AudioSource.playOnAwake = false;
        soundManager.AudioClip = Resources.Load<AudioClip>("Sounds/Music/bgm_festival");

        soundManagerGameObject.SetActive(true);

        // Act
        IAudioControl audioControl = soundManager;
        audioControl.Play();

        // Assert
        Assert.IsNotNull(soundManager.AudioSource.clip, "AudioClip should not be null.");
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
        audioControl.Stop();

        // Assert
        Assert.IsNotNull(soundManager.AudioSource, "AudioClip should not be null. ");
        Assert.IsFalse(soundManager.AudioSource.isPlaying);
    }
}
