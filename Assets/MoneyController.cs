using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ====================================================================================================
/// 
/// 재화 획득 컨트롤러
/// 
/// ====================================================================================================
/// </summary>

[DefaultExecutionOrder(-1000)]
public class MoneyController : MonoBehaviour
{
    public static MoneyController Instance;

    private int _money; // 보유 머니
    public int money
    {
        get => _money;
        set => Handler_Money?.Invoke(_money = value);
    }

    public event Action<float> Handler_Money; // 획득 머니 이벤트


    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 이벤트 발생용
    /// </summary>
    /// <param name="f"></param>
    public void Refresh_Money() => money = money;

    /// <summary>
    /// 재화 획득
    /// </summary>
    public void GetMoney()
    {
        money += 1;
    }
}
