using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemEffect
{
    void ApplyEffect(Player player);
    int Level { get; }
    RewardType RewardType { get; }
    string GetEffectName();

    /// <summary>
    /// ������ RewardType�� ���� ���� ������ �������� �Լ�
    /// FINITE�� ���, maxLevel ���� null ��ȯ
    /// INIFINTE�� ���, maxLevel �̻���ʹ� maxLevel ��ȯ
    /// </summary>
    IItemEffect GetNextReward(Player player);

    Sprite GetItemImage();
    string GetItemDescription();
}

public enum RewardType
{
    FINITE,     // 4�������� ȹ���� �� ���󿡼� ����
    INFINITE,   // ȹ�� ������ ������� ����ؼ� ���� ����Ʈ���� ����
}