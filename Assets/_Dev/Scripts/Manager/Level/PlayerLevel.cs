using System.IO;
using UnityEngine;
using WheelMaker.Common;
using WheelMaker.Manager.Interfaces;
using WheelMaker.Define;
using Newtonsoft.Json;
using System.Linq;

namespace WheelMaker.Manager.Level
{
    public class Level
    {
        public int level;
        public int requirement;
    }
    public class PlayerLevel : MonoBehaviour, ILevel, IRequiredLevel
    {
        [field: SerializeField] public int WheelLevel { get; set; }

        [field: SerializeField] public int ClickLevel { get; set; }

        [field: SerializeField] public int AutoLevel { get; set; }

        public int ByBehaviours(Behaviours behaviours)
        {
            //level
            switch (behaviours)
            {
                case Behaviours.Wheel:
                    {
                        TextAsset requiredLevel = Resources.Load<TextAsset>(Path.Combine(define.path_db, define.Level));
                        var requiredLevelList = JsonConvert.DeserializeObject<Level[]>(requiredLevel.text).ToDictionary(x => x.level, x => x);
                        return requiredLevelList[WheelLevel].requirement; // 이부분 까지
                    }
                    break;
                case Behaviours.MouseClick:
                    Debug.Log("test1");
                    break;
                case Behaviours.Auto:
                    Debug.Log("test2");
                    break;
                default:
                    break;
            }
            return 0;
        }

        public int GetCurrent(Behaviours behaviour)
        {
            int level = 0;
            if (behaviour == Behaviours.Wheel)
            {
                level = WheelLevel;
            }
            else if (behaviour == Behaviours.MouseClick)
            {
                level = ClickLevel;
            }
            else
            {
                level = AutoLevel;
            }
            return level;
        }

        public void Up(Behaviours behaviour)
        {
            if (behaviour == Behaviours.Wheel)
            {
                WheelLevel++;
            }
            else if (behaviour == Behaviours.MouseClick)
            {
                ClickLevel++;
            }
            else
            {
                AutoLevel++;
            }

        }

    }
}