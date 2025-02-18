using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

[System.Serializable]
public class StatBonusList : IEnumerable<StatBonus>
{
    public List<StatBonus> statBonus = new List<StatBonus>();
    
    public StatBonusList(params StatBonus[] stats)
    {
        statBonus.AddRange(stats);
    }

    public IEnumerator<StatBonus> GetEnumerator()
    {
        return statBonus.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public int Count => statBonus.Count;
}

public enum BowType
{
    Normal,
    Red,
    Blue,
    Purple,
}

[CreateAssetMenu()]
public class BowSO : ScriptableObject
{
    [Header("기본 활 정보")]
    public string bowName;
    public BowType bowType;
    public Sprite sprite;

    [Header("업그레이드 정보")]
    public Color color;                         // 업그레이드 단계별 머테리얼
    public List<StatBonusList> StatBonusList;   // 레벨별 스탯 증가량
}
