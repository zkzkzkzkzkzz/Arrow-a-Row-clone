using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Flags]
public enum StatCategory
{
    None        = 0,                                // 아무데서도 나오지 않음
    GateOnly    = 1 << 0,                           // 게이트에서만 나옴
    BossOnly    = 1 << 1,                           // 보스 처치 보상에서만 나옴
    ChestOnly   = 1 << 2,                           // 상자에서만 나옴
    Gate_Boss   = GateOnly | BossOnly,              // 게이트 & 보스 처치
    Gate_Chest  = GateOnly | ChestOnly,             // 게이트 & 상자
    Boss_Chest  = BossOnly | ChestOnly,             // 보스 처치 & 상자
    ALL         = GateOnly | BossOnly | ChestOnly   // 모든 곳에서 나옴
}

public enum StatType
{
    HP,
    MOVESPEED,

    ARROWATK,
    ARROWRATE,
    ARROWSPEED,
    ARROWRANGE,
    ARROWCNT,

    SWORDATK,
    SWORDRATE,
    SWORDSPEED,
    SWORDRANGE,
    SWORDCNT,

    PERCENTAGE,
}

public static class StatTypeExtensions
{
    private static readonly Dictionary<StatType, StatCategory> statCategories = new Dictionary<StatType, StatCategory>
    {
        {StatType.HP,           StatCategory.Gate_Boss},
        {StatType.MOVESPEED,    StatCategory.GateOnly},

        {StatType.ARROWATK,     StatCategory.Gate_Chest},
        {StatType.ARROWRATE,    StatCategory.Gate_Chest},
        {StatType.ARROWSPEED,   StatCategory.Gate_Chest},
        {StatType.ARROWRANGE,   StatCategory.Gate_Chest},
        {StatType.ARROWCNT,     StatCategory.BossOnly},

        {StatType.SWORDATK,     StatCategory.Gate_Chest},
        {StatType.SWORDRATE,    StatCategory.Gate_Chest},
        {StatType.SWORDSPEED,   StatCategory.BossOnly},
        {StatType.SWORDRANGE,   StatCategory.Gate_Chest},
        {StatType.SWORDCNT,     StatCategory.Gate_Chest},

        {StatType.PERCENTAGE, StatCategory.None}
    };

    private static readonly Dictionary<StatType, string> statNames = new Dictionary<StatType, string>
    {
        {StatType.HP,           "HP"},
        {StatType.MOVESPEED,    "가로 속도"},

        {StatType.ARROWATK,     "화살 공격력"},
        {StatType.ARROWRATE,    "화살 빈도"},
        {StatType.ARROWSPEED,   "화살 속도"},
        {StatType.ARROWRANGE,   "화살 거리"},
        {StatType.ARROWCNT,     "화살 개수"},

        {StatType.SWORDATK,     "검 공격력"},
        {StatType.SWORDRATE,    "검 CD"},
        {StatType.SWORDSPEED,   "검 속도"},
        {StatType.SWORDRANGE,   "검 거리"},
        {StatType.SWORDCNT,     "검 개수"},

        {StatType.PERCENTAGE, "화살 공격력 퍼센트"}
    };

    public static bool CanBeAppliedInGate(this StatType stat)
    {
        return (statCategories[stat] & StatCategory.GateOnly) != 0;
    }

    public static bool CanBeAppliedInBoss(this StatType stat)
    {
        return (statCategories[stat] & StatCategory.BossOnly) != 0;
    }

    public static bool CanBeAppliedInChest(this StatType stat)
    {
        return (statCategories[stat] & StatCategory.ChestOnly) != 0;
    }

    public static string GetStatName(this StatType stat)
    {
        return statNames[stat];
    }
}