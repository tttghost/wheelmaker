using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DBManager : Singleton<DBManager>
{
    public DTOBaseData<Gold_Click> Gold_Clicks = new DTOBaseData<Gold_Click>();
    public DTOBaseData<Gold_Auto> Gold_Autos = new DTOBaseData<Gold_Auto>();
    public DTOBaseData<Level> Levels = new DTOBaseData<Level>();

    public MyStatus MyStatus;

    protected override void Awake()
    {
        base.Awake();

        Load_MyStatus();

        GetDB_Level();
        GetDB_Gold();
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
        TextAsset level = Resources.Load<TextAsset>(define.path_db + define.Level);
        Levels.SetDictionary(Levels.LoadTable(level.text).ToDictionary(x => x.level, x => x));
    }

    /// <summary>
    /// 돈 획득
    /// </summary>
    private void GetDB_Gold()
    {
        TextAsset gold = Resources.Load<TextAsset>(define.path_db + define.Gold);

        JObject jobj = JsonConvert.DeserializeObject< JObject>(gold.text);
        foreach (var x in jobj)
        {
            string key = x.Key;
            string value = x.Value.ToString();

            switch (key)
            {
                case nameof(Gold_Click):
                    Gold_Clicks.SetDictionary(Gold_Clicks.LoadTable(value).ToDictionary(x => x.level, x => x));
                    break;
                case nameof(Gold_Auto):
                    Gold_Autos.SetDictionary(Gold_Autos.LoadTable(value).ToDictionary(x => x.level, x => x));
                    break;
                default:
                    break;
            }
        }
    }
}


