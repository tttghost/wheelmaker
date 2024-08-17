using Newtonsoft.Json;

public class MyStatus
{

    public int level_player { get; private set; } = 1;
    public int level_click { get; private set; } = 1;
    public int level_auto { get; private set; } = 1;
    public float gold { get; private set; } = 0;


    public MyStatus()
    {

    }

    // 이 생성자를 JsonConstructor로 지정
    [JsonConstructor]
    public MyStatus(int level_player, int level_click, int level_auto, float gold)
    {
        this.level_player = level_player;
        this.level_click = level_click;
        this.level_auto = level_auto;
        this.gold = gold;
    }

    /// <summary>
    /// 레벨업 플레이어
    /// </summary>
    /// <param name="gold"></param>
    /// <returns></returns>
    public int LevelUp_Player(int gold)
    {
        CostGold(gold);
        return level_player++;
    }

    /// <summary>
    /// 레벨업 클릭
    /// </summary>
    /// <param name="gold"></param>
    /// <returns></returns>
    public int LevelUp_Click(int gold)
    {
        CostGold(gold);
        return level_click++;
    }

    /// <summary>
    /// 레벨업 오토
    /// </summary>
    /// <param name="gold"></param>
    /// <returns></returns>
    public int LevelUp_Auto(int gold)
    {
        CostGold(gold);
        return level_auto++;
    }

    /// <summary>
    /// 재화 획득
    /// </summary>
    /// <param name="gold"></param>
    /// <returns>획득 후 재화</returns>
    public float AddGold(float gold)
    {
        return this.gold += gold;
    }

    /// <summary>
    /// 재화 사용
    /// </summary>
    /// <param name="gold"></param>
    /// <returns>소비 후 재화</returns>
    public float CostGold(float gold)
    {
        return this.gold -= gold;
    }
}

public class Gold
{
    public int level;
    public int gold;
}

public class Gold_Click : Gold
{
}

public class Gold_Auto : Gold
{
}

public class Level
{
    public int level;
    public int requirement;
}