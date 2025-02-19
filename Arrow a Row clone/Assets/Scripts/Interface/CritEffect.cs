using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CritEffect : IItemEffect
{
    public string name;
    public int level;
    public float bonusChance;
    public float bonusDamage;
    
    public CritEffect(int lv, float value1, float value2,string _name = "CritGlasses")
    {
        this.level = lv;
        this.bonusChance = value1;
        this.bonusDamage = value2;
        this.name = _name;
    }

    public static CritEffect[] effects = new CritEffect[]
    {
        new CritEffect(1, 10f, 1.5f),
        new CritEffect(2, 15f, 2f),
        new CritEffect(3, 20f, 2.5f),
        new CritEffect(4, 25f, 3f)
    };

    public static CritEffect GetEffectForLevel(int lv)
    {
        if (lv < 1 || lv > effects.Length)
            return null;

        return effects[lv - 1];
    }

    public void ApplyEffect(Player player)
    {
        player.IncreaseItemStats(ItemType.CRITGLASSESLV, 1);
        player.IncreaseItemStats(ItemType.CRITCHANCE, bonusChance);
        player.IncreaseItemStats(ItemType.CRITDAMAGE, bonusDamage);
    }

    public string GetEffectName()
    {
        return name;
    }

    public int Level { get { return level; } }
}
