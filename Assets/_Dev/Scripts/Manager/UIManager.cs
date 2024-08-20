using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Threading;
using System.Threading.Tasks;

[DefaultExecutionOrder(-9000)]
public class UIManager : Singleton<UIManager>
{
    #region base

    public Stack<panel_Base> stackPanel { get; set; } = new Stack<panel_Base>();
    public ui_Base[] ui_Bases { get;private set; }

    protected override void Awake()
    {
        base.Awake();
        CacheUI();
    }

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += handlerSceneChanged;
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= handlerSceneChanged;
    }

    /// <summary>
    /// 씬 이동 시 UI캐싱 다시
    /// </summary>
    /// <param name="arg0"></param>
    /// <param name="arg1"></param>
    private void handlerSceneChanged(Scene arg0, Scene arg1)
    {
        CacheUI();
    }

    /// <summary>
    /// UI 캐싱
    /// </summary>
    private void CacheUI()
    {
        // 각 ui panel,popup 등... 다시 캐싱
        stackPanel.Clear();
        ui_Bases = FindObjectsOfType<ui_Base>(true);
        ui_Bases.ToList().ForEach(x => x.gameObject.SetActive(true));
        ui_Bases.ToList().ForEach(x => x.gameObject.SetActive(false)); 
        
    }

    #endregion

    #region core

    #region panel

    /// <summary>
    /// 최상위 패널 반환
    /// </summary>
    /// <returns></returns>
    public panel_Base PeekPanel()
    {
        return stackPanel.Peek();
    }

    /// <summary>
    /// 최상위 패널이 이게 맞는지 여부
    /// 당연히 패널카운트가 0 이면 false
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public bool IsPeekPanel<T>() where T : panel_Base
    {
        if(stackPanel.Count == 0)
        {
            return false;
        }
        else
        {
            return stackPanel.Peek() is T;
        }
    }

    public T OpenPanel<T>() where T : panel_Base
    {
        T panel = GetPanel<T>();
        panel.Open();
        return panel;
    }
    public T ClosePanel<T>() where T : panel_Base
    {
        T panel = GetPanel<T>();
        panel.Close();
        return panel;
    }

    /// <summary>
    /// 패널 가져오기
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetPanel<T>() where T : panel_Base
    {
        panel_Base panel_Base = default;
        for (int i = 0; i < ui_Bases.Length; i++)
        {
            panel_Base = ui_Bases[i] as T;
            if (panel_Base)
            {
                break;
            }
        }
        return panel_Base as T;
    }

    /// <summary>
    /// 패널 열기
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T PushPanel<T>() where T : panel_Base
    {
        return GetPanel<T>().Push() as T;
    }

    /// <summary>
    /// 패널 전환
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T SwapPanel<T>() where T : panel_Base
    {
        return GetPanel<T>().Swap() as T;
    }

    /// <summary>
    /// 패널 닫기
    /// </summary>
    /// <returns></returns>
    public panel_Base PopPanel()
    {
        if (stackPanel.Count > 0)
        {
            stackPanel.Pop().Close();
        }

        panel_Base panel_Base = default;

        if (stackPanel.Count > 0)
        {
            panel_Base = stackPanel.Peek();
            panel_Base.Open();
        }

        return panel_Base;
    }

    #endregion

    #endregion

}
