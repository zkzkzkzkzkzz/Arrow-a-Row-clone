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

    // �����׸� IEnumerator �������̽� �����ϱ� ����
    // foreach ������ �����׸� IEnumerable �������̽��� GetEnumerator()�� ȣ���� �� �ֵ��� �䱸�Ѵ�
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    // StatBonusList Ŭ���� �� �б� ���� ������Ƽ
    // List<StatBonus> statBonus�� Count�� ��ȯ�Ѵ�
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
    [Header("�⺻ Ȱ ����")]
    public string bowName;
    public BowType bowType;
    public Sprite sprite;

    [Header("���׷��̵� ����")]
    public Color color;                         // ���׷��̵� �ܰ躰 ���׸���
    public List<StatBonusList> StatBonusList;   // ������ ���� ������
}
