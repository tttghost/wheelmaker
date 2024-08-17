using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panel_Wheel : panel_Base
{
    private Button btn_Wheel;
    private Image img_Question;

    protected override void CacheComponent()
    {
        base.CacheComponent();
        btn_Wheel = gameObject.Search<Button>(nameof(btn_Wheel)).SetData(GoldController.instance.OnClick_GetGold);
        img_Question = gameObject.Search<Image>(nameof(img_Question));
    }
}
