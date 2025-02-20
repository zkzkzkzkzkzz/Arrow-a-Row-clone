using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemEffect
{
    void ApplyEffect(Player player);
    int Level { get; }
    RewardType RewardType { get; }
    string GetEffectName();

    IItemEffect GetNextReward();
}

public enum RewardType
{
    FINITE,     // 4레벨까지 획득할 시 보상에서 제외
    INFINITE,   // 획득 레벨에 상관없이 계속해서 보상 리스트에서 등장
}