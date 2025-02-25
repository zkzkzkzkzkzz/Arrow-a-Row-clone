using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public List<IItemEffect> rewardPool = new List<IItemEffect>();
    [SerializeField] private RewardUI rewardUI;
    [SerializeField] private BowSelectionUI bowSelectionUI;
    
    [SerializeField] private Player player;

    private int bossRewardCount = 0;

    private void Awake()
    {
        {
            rewardPool.Add(CritEffect.GetEffectForLevel(1));
            rewardPool.Add(CapeEffect.GetEffectForLevel(1));
            rewardPool.Add(LifeStealEffect.GetEffectForLevel(1));
            rewardPool.Add(ShieldEffect.GetEffectForLevel(1));
            rewardPool.Add(PenetrationEffect.GetEffectForLevel(1));
        }
    }


    /// <summary>
    /// Chest와 충돌 시 보상 선택 UI를 띄운다
    /// </summary>
    public void ShowRewardSelection()
    {
        if (bossRewardCount == 0)
        {
            player.BoardOn();
        }
        else if (bossRewardCount == 1)
        {
            bowSelectionUI.ShowBows(OnBowSelected);
        }
        else
        {
            List<IItemEffect> selectedRewards = GetRandomRewards(3, CheckAvailableRewards());

            if (rewardUI != null)
                rewardUI.ShowRewards(selectedRewards, OnRewardSelected);
        }

        ++bossRewardCount;
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
            if (effect.RewardType == RewardType.FINITE)
            {
                IItemEffect nextEffect = effect.GetNextReward(player);
                if (nextEffect != null)
                    availableRewards.Add(nextEffect);
            }
            else
            {
                IItemEffect nextEffect = effect.GetNextReward(player);
                availableRewards.Add(nextEffect);
            }
        }

        return availableRewards;
    }

    /// <summary>
    /// BowSelectionUI에서 활 선택 후 호출되는 콜백
    /// 선택된 BowSO를 플레이어에 적용하고, 이후 보상 선택에서
    /// 선택된 BowSO의 업그레이드 보상이 나오도록 설정
    /// </summary>
    private void OnBowSelected(BowSO selectedBow)
    {
        if (selectedBow != null && player != null)
        {
            player.SelectBow(selectedBow);

            BowEffect bowEffect = new BowEffect(2, RewardType.INFINITE, selectedBow, selectedBow.bowName);
            rewardPool.Add(bowEffect);
        }
    }
}
