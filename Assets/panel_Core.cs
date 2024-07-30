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
public class panel_Core : MonoBehaviour
{
    private TMP_Text text_Money; // 재화

    private void Awake()
    {
        text_Money = gameObject.Search<TMP_Text>(nameof(text_Money));
    }

    private void OnEnable()
    {
        MoneyController.Instance.Handler_Money += Callback_Money;
        MoneyController.Instance.Refresh_Money();
    }

    private void OnDisable()
    {
        MoneyController.Instance.Handler_Money -= Callback_Money;
    }

    /// <summary>
    /// 재화획득 콜백
    /// </summary>
    /// <param name="money"></param>
    private void Callback_Money(float money)
    {
        text_Money.text = money.ToString();
    }
}
