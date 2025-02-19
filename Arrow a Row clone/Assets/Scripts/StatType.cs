using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Flags]
public enum StatCategory
{
    None        = 0,                                // �ƹ������� ������ ����
    GateOnly    = 1 << 0,                           // ����Ʈ������ ����
    BossOnly    = 1 << 1,                           // ���� óġ ���󿡼��� ����
    ChestOnly   = 1 << 2,                           // ���ڿ����� ����
    Gate_Boss   = GateOnly | BossOnly,              // ����Ʈ & ���� óġ
    Gate_Chest  = GateOnly | ChestOnly,             // ����Ʈ & ����
    Boss_Chest  = BossOnly | ChestOnly,             // ���� óġ & ����
    ALL         = GateOnly | BossOnly | ChestOnly   // ��� ������ ����
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
        {StatType.MOVESPEED,    "���� �ӵ�"},

        {StatType.ARROWATK,     "ȭ�� ���ݷ�"},
        {StatType.ARROWRATE,    "ȭ�� ��"},
        {StatType.ARROWSPEED,   "ȭ�� �ӵ�"},
        {StatType.ARROWRANGE,   "ȭ�� �Ÿ�"},
        {StatType.ARROWCNT,     "ȭ�� ����"},

        {StatType.SWORDATK,     "�� ���ݷ�"},
        {StatType.SWORDRATE,    "�� CD"},
        {StatType.SWORDSPEED,   "�� �ӵ�"},
        {StatType.SWORDRANGE,   "�� �Ÿ�"},
        {StatType.SWORDCNT,     "�� ����"},

        {StatType.PERCENTAGE, "ȭ�� ���ݷ� �ۼ�Ʈ"}
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