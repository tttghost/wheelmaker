using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
[DefaultExecutionOrder(-10000)]
public class DBManager : Singleton<DBManager>
{
    public DTOBaseData<Localization> data_Localizations = new DTOBaseData<Localization>();
    public DTOBaseData<Gold_Click> data_Gold_Clicks = new DTOBaseData<Gold_Click>();
    public DTOBaseData<Gold_Auto> data_Gold_Autos = new DTOBaseData<Gold_Auto>();
    public DTOBaseData<Level> data_Levels = new DTOBaseData<Level>();

    public MyStatus MyStatus;

    public Util.Handler_Void Handler_Localization;
    protected override void Awake()
    {
        base.Awake();

        Load_MyStatus();

        GetDB_Level();
        GetDB_Gold();
        GetDB_Localization();
    }

    private void OnApplicationQuit()
    {
        Save_MyStatus();
    }

    /// <summary>
    /// 내 상태 로드
    /// </summary>
    public void Load_MyStatus()
    {
        string str = PlayerPrefs.GetString(nameof(MyStatus), string.Empty);
        if (str == string.Empty)
        {
            MyStatus = new MyStatus();
            return;
        }
        MyStatus = JsonConvert.DeserializeObject<MyStatus>(str);
    }

    /// <summary>
    /// 내 상태 세이브
    /// </summary>
    public void Save_MyStatus()
    {
        PlayerPrefs.SetString(nameof(MyStatus), JsonConvert.SerializeObject(MyStatus));
    }

    /// <summary>
    /// 레벨업
    /// </summary>
    private void GetDB_Level()
    {
        TextAsset level = Resources.Load<TextAsset>(Path.Combine(define.path_db, define.Level));
        data_Levels.SetDictionary(data_Levels.LoadTable(level.text).ToDictionary(x => x.level, x => x));
    }

    /// <summary>
    /// 돈 획득
    /// </summary>
    private void GetDB_Gold()
    {
        TextAsset gold = Resources.Load<TextAsset>(Path.Combine(define.path_db, define.Gold));

        JObject jobj = JsonConvert.DeserializeObject< JObject>(gold.text);
        foreach (var x in jobj)
        {
            string key = x.Key;
            string value = x.Value.ToString();

            switch (key)
            {
                case nameof(Gold_Click):
                    data_Gold_Clicks.SetDictionary(data_Gold_Clicks.LoadTable(value).ToDictionary(x => x.level, x => x));
                    break;
                case nameof(Gold_Auto):
                    data_Gold_Autos.SetDictionary(data_Gold_Autos.LoadTable(value).ToDictionary(x => x.level, x => x));
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// 다국어
    /// </summary>
    private void GetDB_Localization()
    {
        TextAsset localization = Resources.Load<TextAsset>(Path.Combine(define.path_db, define.Localization));
        data_Localizations.SetDictionary(data_Localizations.LoadTable(localization.text).ToDictionary(x => x.id, x => x));
    }



    /// <summary>
    /// 다국어 데이터 가져오기
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Localization GetLocalizeData(string id)
    {
        try
        {
            return data_Localizations.GetData(id);
        }
        catch (Exception e)
        {
            Debug.Log("don't localizationData : " + id);
            throw;
        }
    }

    /// <summary>
    /// 언어설정 변경시 갱신
    /// </summary>
    public void RefreshMasterLocalizing()
    {
        Handler_Localization?.Invoke();
    }

}


