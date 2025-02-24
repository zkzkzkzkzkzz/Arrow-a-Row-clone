using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    CRITGLASSESLV,
    CONVERTCAPELV,
    PENETRATIONLV,
    SHIELDLV, 
    LIFESTEALLV,

    CRITCHANCE,        
    CRITDAMAGE,       
    CONVERTDAMAGE,     
    PENETRATIONDAMAGE, 
    SHIELD,            
    LIFESTEAL,  
}

public static class ItemTypeExtensions
{
    private static readonly Dictionary<ItemType, StatCategory> itemStatCategories = new Dictionary<ItemType, StatCategory>
    {
        {ItemType.CRITGLASSESLV,    StatCategory.BossOnly},
        {ItemType.CONVERTCAPELV,    StatCategory.BossOnly},
        {ItemType.PENETRATIONLV,    StatCategory.BossOnly},
        {ItemType.SHIELDLV,         StatCategory.BossOnly},
        {ItemType.LIFESTEALLV,      StatCategory.BossOnly},
         
        {ItemType.CRITCHANCE,           StatCategory.None},
        {ItemType.CRITDAMAGE,           StatCategory.None},
        {ItemType.CONVERTDAMAGE,        StatCategory.None},
        {ItemType.PENETRATIONDAMAGE,    StatCategory.None},
        {ItemType.SHIELD,               StatCategory.None},
        {ItemType.LIFESTEAL,            StatCategory.None}
    };

    private static readonly Dictionary<ItemType, string> itemStatNames = new Dictionary<ItemType, string>
    {
        {ItemType.CRITGLASSESLV,    "치명타 안경"},
        {ItemType.CONVERTCAPELV,    "신속한 망토"},
        {ItemType.PENETRATIONLV,    "관통 장갑"},
        {ItemType.SHIELDLV,         "충격 허리띠"},
        {ItemType.LIFESTEALLV,      "생명력 흡수 목걸이"},

        {ItemType.CRITCHANCE,           "치명타 확률"},
        {ItemType.CRITDAMAGE,           "치명타 데미지"},
        {ItemType.CONVERTDAMAGE,        "화살 속도를 화살 데미지로 전환"},
        {ItemType.PENETRATIONDAMAGE,    "관통 데미지"},
        {ItemType.SHIELD,               "충격 피해 감소"},
        {ItemType.LIFESTEAL,            "생명력 흡수"}
    };

    public static bool CanBeAppliedInGate(this ItemType stat)
    {
        return (itemStatCategories[stat] & StatCategory.GateOnly) != 0;
    }

    public static bool CanBeAppliedInBoss(this ItemType stat)
    {
        return (itemStatCategories[stat] & StatCategory.BossOnly) != 0;
    }

    public static bool CanBeAppliedInChest(this ItemType stat)
    {
        return (itemStatCategories[stat] & StatCategory.ChestOnly) != 0;
    }

    public static string GetStatName(this ItemType stat)
    {
        return itemStatNames[stat];
    }
}