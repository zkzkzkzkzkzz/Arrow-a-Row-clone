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
    public RewardType type;

    private Sprite itemImage;
    private string description;

    public CritEffect(int lv, float value1, float value2, RewardType _type,
                        string image, string _desc, string _name = "CritGlasses")
    {
        this.level = lv;
        this.bonusChance = value1;
        this.bonusDamage = value2;
        this.type = _type;
        this.name = _name;
        this.itemImage = Resources.Load<Sprite>("Image/" + image);
        this.description = _desc;
    }

    public static CritEffect[] effects = new CritEffect[]
    {
        new CritEffect(1, 10f,1.5f, RewardType.FINITE, "glasses", "치명타 확률 10% \n 치명타 데미지 150%"),
        new CritEffect(2, 15f,2f, RewardType.FINITE, "glasses", "치명타 확률 15% \n 치명타 데미지 200%"),
        new CritEffect(3, 20f,2.5f, RewardType.FINITE, "glasses", "치명타 확률 20% \n 치명타 데미지 250%"),
        new CritEffect(4, 25f,3f, RewardType.FINITE, "glasses", "치명타 확률 25% \n 치명타 데미지 300%")
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

    public IItemEffect GetNextReward(Player player)
    {
        int curLv = player.GetPlayerItemStats().CritGlassesLV;
        int nextLv = (curLv == 0) ? 1 : curLv + 1;

        if (nextLv <= 4)
            return GetEffectForLevel(nextLv);

        return null;
    }

    public int Level { get { return level; } }
    public RewardType RewardType { get { return type; } }

    public Sprite GetItemImage()
    {
        return itemImage;
    }

    public string GetItemDescription()
    {
        return description;
    }
}
