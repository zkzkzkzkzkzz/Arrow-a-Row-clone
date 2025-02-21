using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStealEffect : IItemEffect
{
    public string name;
    public int level;
    public float ratio;
    public RewardType type;

    public LifeStealEffect(int lv, float _ratio, RewardType _type, string _name = "LifeSteal")
    {
        this.level = lv;
        this.ratio = _ratio;
        this.type = _type;
        this.name = _name;
    }

    public static LifeStealEffect[] effects = new LifeStealEffect[]
    {
        new LifeStealEffect(1, 4f, RewardType.FINITE),
        new LifeStealEffect(2, 7f, RewardType.FINITE),
        new LifeStealEffect(3, 11f, RewardType.FINITE),
        new LifeStealEffect(4, 16f, RewardType.FINITE)
    };

    public static LifeStealEffect GetEffectForLevel(int lv)
    {
        if (lv < 1 || lv > effects.Length)
            return null;

        return effects[lv - 1];
    }

    public void ApplyEffect(Player player)
    {
        player.IncreaseItemStats(ItemType.LIFESTEALLV, 1);
        player.IncreaseItemStats(ItemType.LIFESTEAL, ratio);
    }

    public string GetEffectName()
    {
        return name;
    }

    public IItemEffect GetNextReward(Player player)
    {
        int curLv = player.GetPlayerItemStats().LifeStealLV;
        int nextLv = (curLv == 0) ? 1 : curLv + 1;

        if (nextLv <= 4)
            return GetEffectForLevel(nextLv);

        return null;
    }

    public int Level { get { return level; } }
    public RewardType RewardType { get { return type; } }
}
