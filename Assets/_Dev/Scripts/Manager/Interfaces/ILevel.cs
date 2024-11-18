using WheelMaker.Common;

namespace WheelMaker.Manager.Interfaces
{
    public interface ILevel
    {
        void Up(Behaviours behaviour);
        int GetCurrent(Behaviours behaviour);
    }
}