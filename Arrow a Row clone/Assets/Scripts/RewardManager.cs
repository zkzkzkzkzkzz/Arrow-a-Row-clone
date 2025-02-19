using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public List<IItemEffect> rewardPool = new List<IItemEffect>();
    [SerializeField] private RewardUI rewardUI;

    [SerializeField] private Player player;

    private void Awake()
    {
        rewardPool.Add(CritEffect.GetEffectForLevel(1));
        rewardPool.Add(CritEffect.GetEffectForLevel(2));
        rewardPool.Add(CritEffect.GetEffectForLevel(3));
        rewardPool.Add(CritEffect.GetEffectForLevel(4));
    }


    /// <summary>
    /// Chest와 충돌 시 보상 선택 UI를 띄운다
    /// </summary>
    public void ShowRewardSelection()
    {
        List<IItemEffect> selectedRewards = GetRandomRewards(3);

        if (rewardUI != null)
            rewardUI.ShowRewards(selectedRewards, OnRewardSelected);
    }

    /// <summary>
    /// reward풀에서 count만큼 랜덤한 보상을 선택하여 반환
    /// </summary>
    private List<IItemEffect> GetRandomRewards(int count)
    {
        List<IItemEffect> selected = new List<IItemEffect>();
        for (int i = 0; i < count; ++i)
        {
            int idx = Random.Range(0, rewardPool.Count);
            selected.Add(rewardPool[idx]);
        }

        return selected;
    }

    /// <summary>
    /// RewardUI에서 보상이 선택되면 호출되는 콜백 함수
    /// </summary>
    private void OnRewardSelected(IItemEffect selectedReward)
    {
        if (selectedReward != null && player != null)
            selectedReward.ApplyEffect(player);
    }
}
