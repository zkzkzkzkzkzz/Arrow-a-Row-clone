using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public List<IItemEffect> rewardPool = new List<IItemEffect>();
    [SerializeField] private RewardUI rewardUI;

    [SerializeField] private Player player;

    private void Awake()
    {
        rewardPool.Add(CritEffect.GetEffectForLevel(1));
        rewardPool.Add(CritEffect.GetEffectForLevel(1));
        rewardPool.Add(CritEffect.GetEffectForLevel(1));
    }


    /// <summary>
    /// Chest와 충돌 시 보상 선택 UI를 띄운다
    /// </summary>
    public void ShowRewardSelection()
    {
        List<IItemEffect> selectedRewards = GetRandomRewards(3, CheckAvailableRewards());

        if (rewardUI != null)
            rewardUI.ShowRewards(selectedRewards, OnRewardSelected);
    }

    /// <summary>
    /// reward풀에서 count만큼 랜덤한 보상을 선택하여 반환
    /// </summary>
    private List<IItemEffect> GetRandomRewards(int count, List<IItemEffect> availableRewards)
    {
        List<IItemEffect> poolCopy = new List<IItemEffect>(availableRewards);
        List<IItemEffect> selected = new List<IItemEffect>();

        count = Mathf.Min(count, poolCopy.Count);

        for (int i = 0; i < count; ++i)
        {
            int idx = Random.Range(0, poolCopy.Count);
            selected.Add(poolCopy[idx]);
            poolCopy.RemoveAt(idx);
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

    /// <summary>
    /// 플레이어가 보유한 상태에 따라 등록 가능한 아이템 판별하는 함수
    /// </summary>
    private List<IItemEffect> CheckAvailableRewards()
    {
        List<IItemEffect> availableRewards = new List<IItemEffect>();

        foreach (IItemEffect effect in rewardPool)
        {
            // 테스트용 CritEffect 타입만 처리
            if (effect is CritEffect)
            {
                int curLv = player.GetPlayerItemStats().CritGlassesLV;

                int nextLv = (curLv == 0) ? 1 : curLv + 1;

                if (nextLv <= 4)
                {
                    CritEffect nextEffect = CritEffect.GetEffectForLevel(nextLv);
                    if (nextEffect != null)
                        availableRewards.Add(nextEffect);
                }
            }
            else
            {
                availableRewards.Add(effect);
            }
        }

        return availableRewards;
    }
}
