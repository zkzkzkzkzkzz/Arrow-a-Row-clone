using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapeEffect : IItemEffect
{
    public string name;
    public int level;
    public float convertRatio;
    public RewardType type;

    private Sprite itemImage;
    private string description;

    public CapeEffect(int level, float _convertRatio, RewardType type,
                    string image, string _desc, string name = "ConvertCape")
    {
        this.name = name;
        this.level = level;
        this.convertRatio = _convertRatio;
        this.description = _desc;
        this.itemImage = Resources.Load<Sprite>("Image/" + image);
        this.type = type;
    }

    public static CapeEffect[] effects = new CapeEffect[]
    {
        new CapeEffect(1, 10f, RewardType.FINITE, "cape", "화살 속도를 화살 데미지로 전환 10%"),
        new CapeEffect(2, 25f, RewardType.FINITE, "cape", "화살 속도를 화살 데미지로 전환 25%"),
        new CapeEffect(3, 40f, RewardType.FINITE, "cape", "화살 속도를 화살 데미지로 전환 40%"),
        new CapeEffect(4, 80f, RewardType.FINITE, "cape", "화살 속도를 화살 데미지로 전환 80%")
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

    public Sprite GetItemImage()
    {
        return itemImage;
    }

    public string GetItemDescription()
    {
        return description;
    }
}
