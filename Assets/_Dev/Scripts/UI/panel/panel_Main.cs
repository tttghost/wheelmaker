using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class panel_Main : panel_Base
{
    protected override void CacheComponent()
    {
        base.CacheComponent();
        Button btn_Start = gameObject.Search<Button>(nameof(btn_Start)).SetData(OnClick_Start);
        Button btn_Option = gameObject.Search<Button>(nameof(btn_Option));
        Button btn_Dictionary = gameObject.Search<Button>(nameof(btn_Dictionary));
        Button btn_Creator = gameObject.Search<Button>(nameof(btn_Creator));
    }

    private void OnClick_Start()
    {
        SceneManager.LoadScene(define.Scene_Game);
    }
}
