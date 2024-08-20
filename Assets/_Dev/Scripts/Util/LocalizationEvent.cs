using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

/// <summary>
/// 다국어를 위한 로컬라이징 이벤트 (컴포넌트에 추가)
/// </summary>
public class LocalizationEvent : MonoBehaviour
{
    [SerializeField] private LocalizionData localizionData = null;
    private string? str = null;

    private bool isNormalString = false;

    #region 핸들러 등록, 삭제

    private void OnEnable()
    {
        DBManager.instance.Handler_Localization += Callback_Localization;
        if (localizionData == null && str == null)
        {
            return;
        }
        Callback_Localization();
    }


    private void OnDisable()
    {
        DBManager.instance.Handler_Localization -= Callback_Localization;
    }
    #endregion



    /// <summary>
    /// MasterLocalData 입력
    /// </summary>
    /// <param name="masterLocalData"></param>
    public void SetLocalizing(LocalizionData masterLocalData)
    {
        isNormalString = false;

        this.localizionData = masterLocalData;
        Callback_Localization();
    }
    /// <summary>
    /// String 입력
    /// </summary>
    /// <param name="str"></param>
    public void SetString(string str)
    {
        isNormalString = true;

        this.str = str;
        Callback_Localization(); // String 입력 시 초기화 해야 할 경우 필요
    }

    private void Callback_Localization()
    {
        if (TryGetComponent(out TMP_Text txtmp))
        {

            if (isNormalString)
            {
                txtmp.text = str;
            }
            else
            {
                if (localizionData != null)
                {
                    txtmp.text = Util.GetLocalization(localizionData);
                }
            }
        }
    }

}

#region packet
public class Localization
{
    public string id; // 다국어 아이디
    public string ko; // 한국어
    public string en; // 영어
}

/// <summary>
/// 컴포넌트에 추가될 데이터
/// </summary>
[Serializable]
public class LocalizionData
{
    public string id;
    public object[] args;

    public LocalizionData() { }

    public LocalizionData(string id, params object[] args)
    {
        this.id = id;
        this.args = args;
    }
}
#endregion