using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 레벨-업 관련 제어 클래스 입니다
/// </summary>
public class LevelUpController : Singleton<LevelUpController>
{
    private MyStatus MyStatus;
    public event Util.Handler_Int Handler_Level_Click; // 클릭레벨 이벤트
    public event Util.Handler_Int Handler_Level_Auto; // 오토레벨 이벤트

    protected override void Awake()
    {
        base.Awake();
        MyStatus = DBManager.instance.MyStatus;
    }
    protected override bool ShouldDontDestroyOnLoad()
    {
        return false;
    }

    /// <summary>
    /// 이벤트 발생용
    /// </summary>
    /// <param name="f"></param>
    public void Refresh_Event()
    {
        Handler_Level_Auto?.Invoke(MyStatus.level_auto);
        Handler_Level_Click?.Invoke(MyStatus.level_click);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="act"></param>
    public void Onclick_Level_Click(Action act = null)
    {
        int nextLevelCost = DBManager.instance.Gold_Clicks.GetData(MyStatus.level_click + 1).gold;
        Handler_Level_Click?.Invoke(MyStatus.LevelUp_Click(nextLevelCost));
        act?.Invoke();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="act"></param>
    public void Onclick_Level_Auto(Action act = null)
    {
        int nextLevelCost = DBManager.instance.Gold_Autos.GetData(MyStatus.level_auto + 1).gold;
        Handler_Level_Auto?.Invoke(MyStatus.LevelUp_Auto(nextLevelCost));
        act?.Invoke();
    }
}
