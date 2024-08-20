using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class panel_LevelUp : panel_Base
{
    protected override void CacheComponent()
    {
        base.CacheComponent();
        iter_LevelUp iter_LevelUp_Prefab = Resources.Load<iter_LevelUp>(Path.Combine(define.path_prefab_ui_iter, nameof(iter_LevelUp)));
        foreach (var lEVELUP_STATE in Util.Enum2EnumArray<LEVELUP_STATE>())
        {
            Instantiate(iter_LevelUp_Prefab, go_Root.transform).SetData(lEVELUP_STATE);
        }
    }
}
