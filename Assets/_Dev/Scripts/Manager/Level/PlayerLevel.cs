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