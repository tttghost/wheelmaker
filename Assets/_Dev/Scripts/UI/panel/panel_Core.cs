using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// ====================================================================================================
/// 
/// 상단 기준이 되는 패널
/// 
/// ====================================================================================================
/// </summary>
public class panel_Core : panel_Base
{
    private TMP_Text text_Level_Auto; // 오토레벨
    private TMP_Text text_Level_Click; // 클릭레벨
    private TMP_Text text_Gold; // 재화
    protected override void CacheComponent()
    {
        base.CacheComponent();
        text_Level_Auto = gameObject.Search<TMP_Text>(nameof(text_Level_Auto));
        text_Level_Click = gameObject.Search<TMP_Text>(nameof(text_Level_Click));
        text_Gold = gameObject.Search<TMP_Text>(nameof(text_Gold));
    }

    private void OnEnable()
    {
        LevelUpController.instance.Handler_Level_Auto += Callback_Level_Auto;
        LevelUpController.instance.Handler_Level_Click += Callback_Level_Click;
        GoldController.instance.Handler_AddGold += Callback_Gold;
        
        LevelUpController.instance.Refresh_Event();
        GoldController.instance.Refresh_Event();
    }

    private void OnDisable()
    {
        LevelUpController.instance.Handler_Level_Auto -= Callback_Level_Auto;
        LevelUpController.instance.Handler_Level_Click -= Callback_Level_Click;
        GoldController.instance.Handler_AddGold -= Callback_Gold;
    }

    /// <summary>
    /// /자동레벨 콜백
    /// </summary>
    /// <param name="level"></param>
    private void Callback_Level_Auto(int level) => text_Level_Auto.text = level.ToString();

    /// <summary>
    /// 클릭레벨 콜백
    /// </summary>
    /// <param name="level"></param>
    private void Callback_Level_Click(int level) => text_Level_Click.text = level.ToString();

    /// <summary>
    /// 재화획득 콜백
    /// </summary>
    /// <param name="gold"></param>
    private void Callback_Gold(int gold) => text_Gold.text = gold.ToString();
}
