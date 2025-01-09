using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WheelMaker.Manager.Interfaces;
using WheelMaker.Manager.Sounds;

public class VolumeTest
{
    [TestCase(0f)]
    [TestCase(0.5f)]
    [TestCase(1f)]
    public void SetVolume(float volume)
    {
        // Arrange
        var go = new GameObject(nameof(SoundManager));
        var soundManager = go.AddComponent<SoundManager>();
        var audioSource = go.AddComponent<AudioSource>();
        var audioClip = Resources.Load<AudioClip>("Sounds/Music/bgm_festival");

        soundManager.Initialize(audioSource, audioClip);

        // Action
        IVolumeControl volumeControl = soundManager;
        volumeControl.Volume = volume;

        // Assert
        Assert.AreEqual(volume, soundManager.AudioSource.volume, "AudioSource volume did not change.");
    }
}
