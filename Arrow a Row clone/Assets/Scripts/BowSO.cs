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
    [Header("�⺻ Ȱ ����")]
    public string bowName;
    public BowType bowType;
    public Sprite sprite;

    [Header("���׷��̵� ����")]
    public List<Material> materials;                        // ���׷��̵� �ܰ躰 ���׸���
    public Dictionary<int, List<StatBonus>> upgradeStats;   // ������ ���� ������
}
