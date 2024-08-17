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
public class GoldController : Singleton<GoldController>
{

    public Animator animator;

    private float duration = 1f; // 1초 동안 진행
    private int steps = 4; // 몇 단계로 나눌 것인지 (예: 4 -> 25씩 증가)

    private MyStatus MyStatus;
    public event Util.Handler_Int Handler_AddGold; // 획득 머니 이벤트

    protected override bool ShouldDontDestroyOnLoad()
    {
        return false;
    }
    protected override void Awake()
    {
        base.Awake();
        MyStatus = DBManager.instance.MyStatus;
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Co_GetGold_Auto());
    }

    protected override void Update()
    {
        base.Update();
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
        Handler_AddGold?.Invoke((int)MyStatus.gold);
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
