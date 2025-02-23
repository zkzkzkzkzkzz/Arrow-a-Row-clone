using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class BowEffect : IItemEffect
{
    public string name;
    public BowSO bow;
    public int level;
    public RewardType type;

    private string ImageName;
    private Sprite itemImage;
    private string description;

    public BowEffect(int lv, RewardType _type, BowSO _bow,
                    string image, string name = "BowUpgrade")
    {
        this.level = lv;
        this.type = _type;
        this.bow = _bow;
        this.ImageName = image;
        this.itemImage = Resources.Load<Sprite>("Image/" + image);
        this.description = BuildDescription(_bow, lv);
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
            return new BowEffect(level + 1, type, bow, ImageName);
        else
            return new BowEffect(maxLevel, type, bow, ImageName);
    }

    public int Level { get { return level; } }
    public BowSO Bow { get { return bow; } }
    public RewardType RewardType { get { return type; } }

    public Sprite GetItemImage()
    {
        return itemImage;
    }

    public string GetItemDescription()
    {
        return description;
    }

    public string GetEffectName()
    {
        return name;
    }

    /// <summary>
    /// BowSO의 StatBonusList를 기반으로
    /// 현재 레벨에 해당하는 스탯 보너스 정보를 문자열로 생성
    /// </summary>
    private string BuildDescription(BowSO bow, int level)
    {
        int idx = level - 1;

        var StatBonusList = bow.StatBonusList[idx];
        StringBuilder sb = new StringBuilder();
        foreach (var bonus in StatBonusList)
        {
            if (bonus.statType == StatType.PERCENTAGE)
                sb.AppendFormat("{0} : {1}%\n", bonus.statType.GetStatName(), bonus.value);
            else
            {
                if (bonus.value >= 0)
                    sb.AppendFormat("{0} +{1}\n", bonus.statType.GetStatName(), bonus.value);
                else
                    sb.AppendFormat("{0} {1}\n", bonus.statType.GetStatName(), bonus.value);
            }
        }

        return sb.ToString();
    }
}
