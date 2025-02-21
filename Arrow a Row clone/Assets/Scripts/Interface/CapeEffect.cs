using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapeEffect : IItemEffect
{
    public string name;
    public int level;
    public float convertRatio;
    public RewardType type;

    public CapeEffect(int level, float _convertRatio, RewardType type, string name = "ConvertCape")
    {
        this.name = name;
        this.level = level;
        this.convertRatio = _convertRatio;
        this.type = type;
    }

    public static CapeEffect[] effects = new CapeEffect[]
    {
        new CapeEffect(1, 10f, RewardType.FINITE),
        new CapeEffect(2, 25f, RewardType.FINITE),
        new CapeEffect(3, 40f, RewardType.FINITE),
        new CapeEffect(4, 80f, RewardType.FINITE)
    };

    public static CapeEffect GetEffectForLevel(int lv)
    {
        if (lv < 1 || lv > effects.Length)
            return null;

        return effects[lv - 1];
    }

    public void ApplyEffect(Player player)
    {
        player.IncreaseItemStats(ItemType.CONVERTCAPELV, 1);
        player.IncreaseItemStats(ItemType.CONVERTDAMAGE, convertRatio);
    }

    public string GetEffectName()
    {
        return name;
    }

    public IItemEffect GetNextReward(Player player)
    {
        int curLv = player.GetPlayerItemStats().ConvertCapeLV;
        int nextLv = (curLv == 0) ? 1 : curLv + 1;

        if (nextLv <= 4)
            return GetEffectForLevel(nextLv);

        return null;
    }

    public int Level { get { return level; } }
    public RewardType RewardType { get { return type; } }
}
