using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panel_Wheel : panel_Base
{
    private Button btn_Wheel;
    private Animator animator_Wheel;
    private Image img_Question;

    protected override void CacheComponent()
    {
        base.CacheComponent();
        btn_Wheel = gameObject.Search<Button>(nameof(btn_Wheel)).SetData(GoldController.instance.OnClick_GetGold);
        animator_Wheel = btn_Wheel.GetComponent<Animator>();
        img_Question = gameObject.Search<Image>(nameof(img_Question));
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
        animator_Wheel.Play(level.ToString());

        if (DBManager.instance.MyStatus.level_wheel < level)
        {
            btn_Wheel.image.color = Color.black;
            img_Question.enabled = true;
        }
        else
        {
            btn_Wheel.image.color = Color.white;
            img_Question.enabled = false;
        }
    }
}
