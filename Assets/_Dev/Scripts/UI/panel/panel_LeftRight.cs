using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class panel_LeftRight : panel_Base
{
    private Button btn_Left;
    private Button btn_Right;

    protected override void CacheComponent()
    {
        base.CacheComponent();
        btn_Left = gameObject.Search<Button>(nameof(btn_Left)).SetData(OnClick_Left);
        btn_Right = gameObject.Search<Button>(nameof(btn_Right)).SetData(OnClick_Right);
    }

    private void OnClick_Right()
    {
        LevelUpController.instance.OnChangedValue_Level(+1);
    }

    private void OnClick_Left()
    {
        LevelUpController.instance.OnChangedValue_Level(-1);
    }

    private void OnEnable()
    {
        LevelUpController.instance.Handler_Level_Wheel += Callback_Level_Wheel;
        Callback_Level_Wheel(DBManager.instance.MyStatus.level_wheel);
    }

    private void OnDisable()
    {
        LevelUpController.instance.Handler_Level_Wheel -= Callback_Level_Wheel;
    }

    private void Callback_Level_Wheel(int level)
    {
        btn_Left.interactable = 1 < level;
        int maxLevel = DBManager.instance.data_Levels.GetList().Max(x => x.level);
        btn_Right.interactable = level < maxLevel + 1;


    }
}
