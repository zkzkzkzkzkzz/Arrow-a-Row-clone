using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowEffect : IItemEffect
{
    public string name;
    public BowSO bow;
    public int level;
    public RewardType type;

    public BowEffect(int lv, RewardType _type, BowSO _bow, string name = "BowUpgrade")
    {
        this.level = lv;
        this.type = _type;
        this.bow = _bow;
        this.name = name;
    }

    public void ApplyEffect(Player player)
    {
        player.UpgradeBow(bow);
    }

    public IItemEffect GetNextReward(Player player)
    {
        int maxLevel = 4;
        if (level < maxLevel)
            return new BowEffect(level + 1, type, bow);
        else
            return new BowEffect(maxLevel, type, bow);
    }

    public int Level { get { return level; } }
    public BowSO Bow { get { return bow; } }
    public RewardType RewardType { get { return type; } }

    public string GetEffectName()
    {
        return name;
    }
}
