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
        soundManager.AudioClip = Resources.Load<AudioClip>("Sounds/Music/bgm_festival.mp3");

        soundManagerGameObject.SetActive(true);

        // Act
        IAudioControl audioControl = soundManager;
        audioControl.Play();

        // Assert
        Assert.AreEqual(soundManager.AudioSource.isPlaying, true);
    }

    [Test]
    public void Stop()
    {
        // Arrange
        GameObject soundManagerGameObject = new GameObject("SoundManager");
        soundManagerGameObject.SetActive(false);

        SoundManager soundManager = soundManagerGameObject.AddComponent<SoundManager>();
        soundManager.AudioSource = soundManagerGameObject.AddComponent<AudioSource>();
        soundManager.AudioClip = Resources.Load<AudioClip>("Sounds/Music/bgm_festival.mp3");

        soundManagerGameObject.SetActive(true);

        // Act
        IAudioControl audioControl = soundManager;
        audioControl.Stop();

        // Assert
        Assert.AreEqual(soundManager.AudioSource.isPlaying, false);
    }
}
