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
    /// Chest�� �浹 �� ���� ���� UI�� ����
    /// </summary>
    public void ShowRewardSelection()
    {
        List<IItemEffect> selectedRewards = GetRandomRewards(3);

        if (rewardUI != null)
            rewardUI.ShowRewards(selectedRewards, OnRewardSelected);
    }

    /// <summary>
    /// rewardǮ���� count��ŭ ������ ������ �����Ͽ� ��ȯ
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
    /// RewardUI���� ������ ���õǸ� ȣ��Ǵ� �ݹ� �Լ�
    /// </summary>
    private void OnRewardSelected(IItemEffect selectedReward)
    {
        if (selectedReward != null && player != null)
            selectedReward.ApplyEffect(player);
    }
}
