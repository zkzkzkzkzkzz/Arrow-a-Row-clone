using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatBonus
{
    public StatType statType;
    public float value;

    public StatBonus(StatType type, float val)
    {
        statType = type;
        value = val;
    }
}

public enum BowType
{
    Normal,
    Red,
    Blue,
    Purple,
}

public class BowSO : ScriptableObject
{
    [Header("기본 활 정보")]
    public string bowName;
    public BowType bowType;
    public Sprite sprite;

    [Header("업그레이드 정보")]
    public List<Material> materials;                        // 업그레이드 단계별 머테리얼
    public Dictionary<int, List<StatBonus>> upgradeStats;   // 레벨별 스탯 증가량
}
