using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEffect : IItemEffect
{
    public string name;
    public int level;
    public float ratio;
    public RewardType type;

    public ShieldEffect(int lv, float _ratio, RewardType _type, string _name = "Shield")
    {
        this.level = lv;
        this.ratio = _ratio;
        this.type = _type;
        this.name = _name;
    }

    public static ShieldEffect[] effects = new ShieldEffect[]
    {
        new ShieldEffect(1, 20f, RewardType.FINITE),
        new ShieldEffect(2, 30f, RewardType.FINITE),
        new ShieldEffect(3, 50f, RewardType.FINITE),
        new ShieldEffect(4, 80f, RewardType.FINITE)
    };

    public static ShieldEffect GetEffectForLevel(int lv)
    {
        if (lv < 1 || lv > effects.Length)
            return null;

        return effects[lv - 1];
    }

    public void ApplyEffect(Player player)
    {
        player.IncreaseItemStats(ItemType.SHIELDLV, 1);
        player.IncreaseItemStats(ItemType.SHIELD, ratio);
    }

    public string GetEffectName()
    {
        return name;
    }

    public IItemEffect GetNextReward(Player player)
    {
        int curLv = player.GetPlayerItemStats().ShieldLV;
        int nextLv = (curLv == 0) ? 1 : curLv + 1;

        if (nextLv <= 4)
            return GetEffectForLevel(nextLv);

        return null;
    }

    public int Level { get { return level; } }
    public RewardType RewardType { get { return type; } }
}
