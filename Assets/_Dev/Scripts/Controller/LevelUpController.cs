using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 레벨-업 컨트롤러 클래스
/// </summary>
public class LevelUpController : Singleton<LevelUpController>
{
    private MyStatus MyStatus;
    public event Util.Handler_Int Handler_Level_Wheel; // 휠캐릭터 레벨 이벤트
    public event Util.Handler_Int Handler_Level_Click; // 클릭 레벨 이벤트
    public event Util.Handler_Int Handler_Level_Auto; // 오토 레벨 이벤트

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
    public void OnClick_Level_Wheel(Action act = null)
    {
        int nextLevelCost = DBManager.instance.data_Levels.GetData(MyStatus.level_wheel + 1).requirement;
        if(CheckCost(nextLevelCost) == false) return;
        int nextLevel = MyStatus.LevelUp_Wheel(nextLevelCost);
        Handler_Level_Wheel?.Invoke(nextLevel);
        act?.Invoke();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="act"></param>
    public void OnClick_Level_Click(Action act = null)
    {
        int nextLevelCost = DBManager.instance.data_Gold_Clicks.GetData(MyStatus.level_click + 1).requirement;
        if (CheckCost(nextLevelCost) == false) return;
        int nextLevel = MyStatus.LevelUp_Click(nextLevelCost);
        Handler_Level_Click?.Invoke(nextLevel);
        act?.Invoke();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="act"></param>
    public void OnClick_Level_Auto(Action act = null)
    {
        int nextLevelCost = DBManager.instance.data_Gold_Autos.GetData(MyStatus.level_auto + 1).requirement;
        if (CheckCost(nextLevelCost) == false) return;
        int nextLevel = MyStatus.LevelUp_Auto(nextLevelCost);
        Handler_Level_Auto?.Invoke(nextLevel);
        act?.Invoke();
    }

    /// <summary>
    /// 레벨업이 가능한지 계산
    /// </summary>
    /// <param name="cost"></param>
    /// <returns></returns>
    public bool CheckCost(int cost)
    {
        return MyStatus.gold >= cost;
    }
}
