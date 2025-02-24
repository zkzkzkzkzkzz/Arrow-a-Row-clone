using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetrationEffect : IItemEffect
{
    public string name;
    public int level;
    public float ratio;
    public RewardType type;

    private Sprite itemImage;
    private string description;

    public PenetrationEffect(int lv, float _ratio, RewardType _type,
                            string image, string _desc, string _name = "Penetration")
    {
        this.level = lv;
        this.ratio = _ratio;
        this.type = _type;
        this.itemImage = Resources.Load<Sprite>("Image/" + image);
        this.description = _desc;
        this.name = _name;
    }

    public static PenetrationEffect[] effects = new PenetrationEffect[]
    {
        new PenetrationEffect(1, 5f, RewardType.FINITE, "penetration", "관통 가능 1회 \n 관통 데미지 5%"),
        new PenetrationEffect(2, 10f, RewardType.FINITE, "penetration", "관통 가능 1회 \n 관통 데미지 10%"),
        new PenetrationEffect(3, 25f, RewardType.FINITE, "penetration", "관통 가능 1회 \n 관통 데미지 25%"),
        new PenetrationEffect(4, 40f, RewardType.FINITE, "penetration", "관통 가능 1회 \n 관통 데미지 40%")
    };

    public static PenetrationEffect GetEffectForLevel(int lv)
    {
        if (lv < 1 || lv > effects.Length)
            return null;

        return effects[lv - 1];
    }

    public void ApplyEffect(Player player)
    {
        player.IncreaseItemStats(ItemType.PENETRATIONLV, 1);
        player.IncreaseItemStats(ItemType.PENETRATIONDAMAGE, ratio);
    }

    public string GetEffectName()
    {
        return name;
    }

    public IItemEffect GetNextReward(Player player)
    {
        int curLv = player.GetPlayerItemStats().PenetrationLV;
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
