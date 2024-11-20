using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using WheelMaker.Common;
using WheelMaker.Manager.Level;
using WheelMaker.Define;

public class panel_LevelUp : panel_Base
{
    [field: SerializeField] public PlayerLevel playerLevel { get; set; }
    protected override void CacheComponent()
    {
        base.CacheComponent();

        var prefabName = Path.Combine(define.path_prefab_ui_iter, nameof(iter_LevelUp));
        var levelUpPrefab = Resources.Load<iter_LevelUp>(prefabName);
        var behavioursArray = Util.Enum2EnumArray<Behaviours>();

        foreach (var behaviours in behavioursArray)
        {
            Instantiate(levelUpPrefab, go_Root.transform).Initialize(playerLevel, behaviours);
        }
    }
}
