using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemEffect
{
    void ApplyEffect(Player player);
    int Level { get; }
    RewardType RewardType { get; }
    string GetEffectName();

    IItemEffect GetNextReward(Player player);
}

public enum RewardType
{
    FINITE,     // 4�������� ȹ���� �� ���󿡼� ����
    INFINITE,   // ȹ�� ������ ������� ����ؼ� ���� ����Ʈ���� ����
}