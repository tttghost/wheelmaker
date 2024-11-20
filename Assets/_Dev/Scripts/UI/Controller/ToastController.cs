using TMPro;
using UnityEngine;

public class ToastController : IToast
{
    private GameObject _toastObject;
    private bool _isOpen;

    public ToastController(string toastObjectName)
    {
        // Resources.Load를 통해 이름으로 GameObject를 로드하고 인스턴스화
        GameObject toastPrefab = Resources.Load<GameObject>(toastObjectName);

        if (toastPrefab != null)
        {
            _toastObject = GameObject.Instantiate(toastPrefab);
            _toastObject.SetActive(false); // 처음에는 비활성화 상태
        }
        else
        {
            Debug.LogError($"{toastObjectName} prefab not found in Resources folder.");
        }

        _isOpen = false;
    }

    public void Open(string message)
    {
        if (_toastObject != null)
        {
            _toastObject.SetActive(true);
            _toastObject.GetComponentInChildren<TMP_Text>().text = message; // 메시지 설정
            _isOpen = true;
        }
    }

    public void Close()
    {
        if (_toastObject != null)
        {
            _toastObject.SetActive(false);
            _isOpen = false;
        }
    }

    public bool IsOpen => _isOpen;
}
