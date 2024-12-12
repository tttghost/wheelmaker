using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTitle : MonoBehaviour
{
    [field: SerializeField] public SoundManager SoundManager { get; set; }

    private IAudioControl audioControl; // 재생, 멈춤 인터페이스 구현

    private void Awake()
    {
        DBManager.instance.Init();
    }

    /// <summary>
    /// 사운드매니저 주입
    /// </summary>
    private void InjectSoundManager(SoundManager soundManager)
    {
        audioControl?.Stop(); // 기존 오디오 중지
        audioControl = soundManager;
        // audioControl?.Play(); // 오디오 재생 을 고려해봤는데 중지만 넣는게 좋겠다고 판단
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("사운드매니저 주입");
            InjectSoundManager(SoundManager); // 주입
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("사운드매니저 제거");
            InjectSoundManager(null); // 제거
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            audioControl?.Play(); // 오디오 재생
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            audioControl?.Stop(); // 오디오 중지
        }
    }
}
