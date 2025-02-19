using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CritEffect : IItemEffect
{
    public float bonusChance;
    public float bonusDamage;
    
    public CritEffect(float value1, float value2)
    {
        this.bonusChance = value1;
        this.bonusDamage = value2;
    }

    public static Dictionary<int, CritEffect> effects = new Dictionary<int, CritEffect>
    {
        {0, new CritEffect(0f, 0f)},
        {1, new CritEffect(10f, 1.5f)},
        {2, new CritEffect(15f, 2f)},
        {3, new CritEffect(20f, 2.5f)},
        {4, new CritEffect(25f, 3f)},
    };

    public static CritEffect GetEffectForLevel(int lv)
    {
        if (effects.TryGetValue(lv, out CritEffect effect))
            return effect;

        return null;
    }

    public void ApplyEffect(Player player)
    {
        player.IncreaseItemStats(ItemType.CRITCHANCE, bonusChance);
        player.IncreaseItemStats(ItemType.CRITDAMAGE, bonusDamage);
    }
}
