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
        {ItemType.CRITGLASSESLV,    "ġ��Ÿ �Ȱ�"},
        {ItemType.CONVERTCAPELV,    "�ż��� ����"},
        {ItemType.PENETRATIONLV,    "���� �尩"},
        {ItemType.SHIELDLV,         "��� �㸮��"},
        {ItemType.LIFESTEALLV,      "����� ��� �����"},

        {ItemType.CRITCHANCE,           "ġ��Ÿ Ȯ��"},
        {ItemType.CRITDAMAGE,           "ġ��Ÿ ������"},
        {ItemType.CONVERTDAMAGE,        "ȭ�� �ӵ��� ȭ�� �������� ��ȯ"},
        {ItemType.PENETRATIONDAMAGE,    "���� ������"},
        {ItemType.SHIELD,               "��� ���� ����"},
        {ItemType.LIFESTEAL,            "����� ���"}
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