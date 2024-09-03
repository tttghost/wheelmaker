using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class iter_LevelUp : ui_Base
{
    private TMP_Text text_Title;
    private TMP_Text text_Summary;
    private TMP_Text text_Plus;
    private TMP_Text text_LevelUp;

    private Button btn_LevelUp;

    private LEVELUP_STATE lEVELUP_STATE;

    protected override void CacheComponent()
    {
        base.CacheComponent();
        text_Title = gameObject.Search<TMP_Text>(nameof(text_Title));
        text_Summary = gameObject.Search<TMP_Text>(nameof(text_Summary));
        text_Plus = gameObject.Search<TMP_Text>(nameof(text_Plus));
        text_LevelUp = gameObject.Search<TMP_Text>(nameof(text_LevelUp));

        btn_LevelUp = gameObject.Search<Button>(nameof(btn_LevelUp)).SetData(OnClick_LevelUp);
    }


    /// <summary>
    /// 
    /// </summary>
    private void OnClick_LevelUp()
    {
        switch (lEVELUP_STATE)
        {
            case LEVELUP_STATE.WHEEL: LevelUpController.instance.OnClick_Level_Wheel(SetNextLevelCost); break;
            case LEVELUP_STATE.CLICK: LevelUpController.instance.OnClick_Level_Click(SetNextLevelCost); break;
            case LEVELUP_STATE.AUTO: LevelUpController.instance.OnClick_Level_Auto(SetNextLevelCost); break;
            default:
                break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="lEVELUP_STATE"></param>
    public void SetData(LEVELUP_STATE lEVELUP_STATE)
    {
        this.lEVELUP_STATE = lEVELUP_STATE;

        switch (lEVELUP_STATE)
        {
            case LEVELUP_STATE.WHEEL:
                text_Title.SetData(new LocalizionData("wheel_levelup_title"));
                text_Summary.SetData(new LocalizionData("wheel_levelup_summary"));
                break;
            case LEVELUP_STATE.CLICK:
                text_Title.SetData(new LocalizionData("click_levelup_title"));
                text_Summary.SetData(new LocalizionData("click_levelup_summary"));
                break;
            case LEVELUP_STATE.AUTO:
                text_Title.SetData(new LocalizionData("auto_levelup_title"));
                text_Summary.SetData(new LocalizionData("auto_levelup_summary"));
                break;
            default:
                break;
        }

        SetNextLevelCost();
    }

    /// <summary>
    /// 다음레벨에 필요한 코스트
    /// </summary>
    private void SetNextLevelCost()
    {
        // 다음 레벨
        int currentLevel = default;
        int plus = default;
        switch (lEVELUP_STATE)
        {
            case LEVELUP_STATE.WHEEL:
                currentLevel = DBManager.instance.MyStatus.level_wheel;
                break;
            case LEVELUP_STATE.CLICK:
                currentLevel = DBManager.instance.MyStatus.level_click;
                plus = DBManager.instance.data_Gold_Clicks.GetData(currentLevel).gold;
                break;
            case LEVELUP_STATE.AUTO:
                currentLevel = DBManager.instance.MyStatus.level_auto;
                plus = DBManager.instance.data_Gold_Autos.GetData(currentLevel).gold;
                break;
            default:
                break;
        }
        text_Plus.text = plus == default ? default : $"(+{plus.ToString("N0")})";
        int nextLevel = currentLevel + 1;

        Level level = IsExistLevel(nextLevel);
        // 다음레벨이 존재하지 않는다면
        if (level == default)
        {
            text_LevelUp.text = $"Lv Max";
        }
        else
        {
            text_LevelUp.text = $"Lv UP {currentLevel} → {nextLevel}\n<b>{level.requirement.ToString("N0")}</b>";
        }
    }

    /// <summary>
    /// 레벨 존재 여부
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    private Level IsExistLevel(int level)
    {
        Level isExistLevel = default;
        //다음레벨에 필요한 코스트
        switch (lEVELUP_STATE)
        {
            case LEVELUP_STATE.WHEEL: isExistLevel = DBManager.instance.data_Levels.GetData(level); break;
            case LEVELUP_STATE.CLICK: isExistLevel = DBManager.instance.data_Gold_Clicks.GetData(level); break;
            case LEVELUP_STATE.AUTO: isExistLevel = DBManager.instance.data_Gold_Autos.GetData(level); break;
        }
        return isExistLevel;
    }

    private void OnEnable()
    {
        GoldController.instance.Handler_AddGold += Callback_AddGold;
    }

    private void OnDisable()
    {
        GoldController.instance.Handler_AddGold -= Callback_AddGold;
    }

    /// <summary>
    /// 돈을 얻었을 때 콜백
    /// 버튼 enable/disable
    /// </summary>
    /// <param name="i"></param>
    private void Callback_AddGold(int i)
    {
        int nextLevel = default;
        switch (lEVELUP_STATE)
        {
            case LEVELUP_STATE.WHEEL: nextLevel = DBManager.instance.MyStatus.level_wheel + 1; break;
            case LEVELUP_STATE.CLICK: nextLevel = DBManager.instance.MyStatus.level_click + 1; break;
            case LEVELUP_STATE.AUTO: nextLevel = DBManager.instance.MyStatus.level_auto + 1; break;
        }

        // 다음레벨이 존재하지 않는다면
        Level isExistNextLevel = IsExistLevel(nextLevel);
        if(isExistNextLevel == default)
        {
            canvasGroup.interactable = false;
            btn_LevelUp.interactable = false;
            return;
        }

        // 다음레벨 존재 시
        int nextCost = default;
        switch (lEVELUP_STATE)
        {
            case LEVELUP_STATE.WHEEL: nextCost = DBManager.instance.data_Levels.GetData(nextLevel).requirement; break;
            case LEVELUP_STATE.CLICK: nextCost = DBManager.instance.data_Gold_Clicks.GetData(nextLevel).requirement; break;
            case LEVELUP_STATE.AUTO: nextCost = DBManager.instance.data_Gold_Autos.GetData(nextLevel).requirement; break;
        }

        btn_LevelUp.interactable = LevelUpController.instance.CheckCost(nextCost);
    }
}
