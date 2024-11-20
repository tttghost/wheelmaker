using Newtonsoft.Json;

public class MyStatus
{

    public int level_wheel { get; private set; } = 1;
    public int level_click { get; private set; } = 1;
    public int level_auto { get; private set; } = 1;
    public float gold { get; private set; } = 0;


    public MyStatus()
    {

    }

    // 이 생성자를 JsonConstructor로 지정
    [JsonConstructor]
    public MyStatus(int level_wheel, int level_click, int level_auto, float gold)
    {
        this.level_wheel = level_wheel;
        this.level_click = level_click;
        this.level_auto = level_auto;
        this.gold = gold;
    }

    /// <summary>
    /// 레벨업 휠 캐릭터
    /// </summary>
    /// <param name="gold"></param>
    /// <returns></returns>
    public int LevelUp_Wheel(int gold)
    {
        CostGold(gold);
        return ++level_wheel;
    }

    /// <summary>
    /// 레벨업 클릭
    /// </summary>
    /// <param name="gold"></param>
    /// <returns></returns>
    public int LevelUp_Click(int gold)
    {
        CostGold(gold);
        return ++level_click;
    }

    /// <summary>
    /// 레벨업 오토
    /// </summary>
    /// <param name="gold"></param>
    /// <returns></returns>
    public int LevelUp_Auto(int gold)
    {
        CostGold(gold);
        return ++level_auto;
    }

    /// <summary>
    /// 재화 획득
    /// </summary>
    /// <param name="gold"></param>
    /// <returns>획득 후 재화</returns>
    public float AddGold(float gold)
    {
        this.gold += gold;
        return this.gold;
    }

    /// <summary>
    /// 재화 사용
    /// </summary>
    /// <param name="gold"></param>
    /// <returns>소비 후 재화</returns>
    public void CostGold(float gold)
    {
        this.gold -= gold;
    }
}


public class Level
{
    public int level;
    public int requirement;
}

public class Gold : Level
{
    public int gold;
}

public class Gold_Click : Gold
{
}

public class Gold_Auto : Gold
{
}