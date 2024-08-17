using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ====================================================================================================
/// 
/// 재화 획득 컨트롤러
/// 
/// ====================================================================================================
/// </summary>
[DefaultExecutionOrder(-1000)]
public class GoldController : MonoBehaviour
{
    public static GoldController Instance;

    private float duration = 1f; // 1초 동안 진행
    private int steps = 4; // 몇 단계로 나눌 것인지 (예: 4 -> 25씩 증가)

    private MyStatus MyStatus;
    public event Util.Handler_Int Handler_AddGold; // 획득 머니 이벤트
    public event Util.Handler_Int Handler_Level_Click; // 클릭레벨 이벤트
    public event Util.Handler_Int Handler_Level_Auto; // 오토레벨 이벤트
    private void Awake()
    {
        Instance = this;
        MyStatus = DBManager.instance.MyStatus;
    }

    private void Start()
    {
        StartCoroutine(Co_GetGold_Auto());
    }

    public Animator animator;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.Play("1");
            animator.GetComponent<Image>().color = Color.white;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animator.Play("2");
            animator.GetComponent<Image>().color = Color.black;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            animator.Play("3");
            animator.GetComponent<Image>().color = Color.black;
        }
    }

    /// <summary>
    /// 이벤트 발생용
    /// </summary>
    /// <param name="f"></param>
    public void Refresh_Event()
    {
        Handler_Level_Auto?.Invoke(MyStatus.level_auto);
        Handler_Level_Click?.Invoke(MyStatus.level_click);
        Handler_AddGold?.Invoke((int)MyStatus.gold);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="act"></param>
    public void Onclick_Level_Click(Action act = null)
    {
        int nextLevelCost = DBManager.instance.Gold_Clicks.GetData(MyStatus.level_click + 1).gold;
        Handler_Level_Click?.Invoke(MyStatus.LevelUp_Click(nextLevelCost));
        act?.Invoke();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="act"></param>
    public void Onclick_Level_Auto(Action act = null)
    {
        int nextLevelCost = DBManager.instance.Gold_Autos.GetData(MyStatus.level_auto + 1).gold;
        Handler_Level_Auto?.Invoke(MyStatus.LevelUp_Auto(nextLevelCost));
        act?.Invoke();
    }

    /// <summary>
    /// 현 레벨에 맞게 클릭시 재화 획득
    /// </summary>
    public void OnClick_GetGold()
    {
        int level = MyStatus.level_click;
        int get = DBManager.instance.Gold_Clicks.GetData(level).gold;
        AddGold(get);
    }

    /// <summary>
    /// 현 레벨에 맞게 자동으로 재화 획득
    /// </summary>
    /// <returns></returns>
    private IEnumerator Co_GetGold_Auto()
    {
        while (true)
        {
            float gold = (float)DBManager.instance.Gold_Autos.GetData(MyStatus.level_auto).gold / steps;
            AddGold(gold);
            yield return new WaitForSeconds(duration / steps);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="gold"></param>
    private void AddGold(float gold)
    {
        Handler_AddGold?.Invoke((int)MyStatus.AddGold(gold));
    }

}
