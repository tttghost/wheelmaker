using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 싱글톤매니저 제네릭
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance = null;
    public static T instance
    {
        get
        {
            //인스턴스 존재여부 확인
            if (_instance == null)
            {
                GameObject go = null;
                T t = FindObjectOfType<T>(true);

                if (t == null)
                {
                    go = new GameObject();
                    t = go.AddComponent<T>();
                }
                else 
                {
                    go = t.gameObject;
                }
                _instance = t;

                //기본세팅
                go.SetActive(true);
                go.name = typeof(T).Name;
                go.transform.position = Vector3.zero;
                go.transform.rotation= Quaternion.identity;

                //DontDestroy 처리
                DontDestroyOnLoad(go);
            }

            //중복제거
            T[] ts = FindObjectsOfType<T>(true);
            foreach (var t in ts)
            {
                if (_instance != t)
                {
                    Destroy(t.gameObject);
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        _instance = instance; //초기 instance의 get 실행용
    }
    protected virtual void Start()
    {

    }
    protected virtual void Update()
    {

    }

    /// <summary>
    /// 강제로 init가능
    /// </summary>
    public void Init() { }
}
