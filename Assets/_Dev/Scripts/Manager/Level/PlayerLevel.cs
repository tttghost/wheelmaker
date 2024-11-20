using UnityEngine;
using WheelMaker.Common;
using WheelMaker.Manager.Interfaces;

namespace WheelMaker.Manager.Level
{
    public class PlayerLevel : MonoBehaviour, ILevel
    {
        [field: SerializeField] public int ClickLevel { get; set; }

        [field: SerializeField] public int WheelLevel { get; set; }

        [field: SerializeField] public int AutoLevel { get; set; }

        public int GetCurrent(Behaviours behaviour)
        {
            int level = 0;
            if (behaviour == Behaviours.MouseWheel)
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
            if (behaviour == Behaviours.MouseWheel)
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