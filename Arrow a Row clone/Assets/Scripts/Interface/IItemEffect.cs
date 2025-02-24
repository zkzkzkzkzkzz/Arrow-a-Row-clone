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
    /// 아이템 RewardType에 따라 다음 보상을 가져오는 함수
    /// FINITE일 경우, maxLevel 이후 null 반환
    /// INIFINTE일 경우, maxLevel 이상부터는 maxLevel 반환
    /// </summary>
    IItemEffect GetNextReward(Player player);

    Sprite GetItemImage();
    string GetItemDescription();
}

public enum RewardType
{
    FINITE,     // 4레벨까지 획득할 시 보상에서 제외
    INFINITE,   // 획득 레벨에 상관없이 계속해서 보상 리스트에서 등장
}