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
    /// Chest�� �浹 �� ���� ���� UI�� ����
    /// </summary>
    public void ShowRewardSelection()
    {
        List<IItemEffect> selectedRewards = GetRandomRewards(3, CheckAvailableRewards());

        if (rewardUI != null)
            rewardUI.ShowRewards(selectedRewards, OnRewardSelected);
    }

    /// <summary>
    /// rewardǮ���� count��ŭ ������ ������ �����Ͽ� ��ȯ
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
    /// RewardUI���� ������ ���õǸ� ȣ��Ǵ� �ݹ� �Լ�
    /// </summary>
    private void OnRewardSelected(IItemEffect selectedReward)
    {
        if (selectedReward != null && player != null)
            selectedReward.ApplyEffect(player);
    }

    /// <summary>
    /// �÷��̾ ������ ���¿� ���� ��� ������ ������ �Ǻ��ϴ� �Լ�
    /// </summary>
    private List<IItemEffect> CheckAvailableRewards()
    {
        List<IItemEffect> availableRewards = new List<IItemEffect>();

        foreach (IItemEffect effect in rewardPool)
        {
            // �׽�Ʈ�� CritEffect Ÿ�Ը� ó��
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
