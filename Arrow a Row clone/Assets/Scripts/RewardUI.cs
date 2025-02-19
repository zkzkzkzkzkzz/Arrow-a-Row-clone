using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RewardUI : MonoBehaviour
{
    [SerializeField] private GameObject rewardPanel;
    [SerializeField] private Button[] rewardButtons;

    // ���� ���� �� ȣ���� �ݹ�
    private Action<IItemEffect> onRewardSelectedCallback;

    // ���� ǥ�� ���� ���� ���
    private List<IItemEffect> curRewards;


    /// <summary>
    /// ���� ��ϰ� ���� �� ȣ���� �ݹ��� �޾� �г� Ȱ��ȭ
    /// </summary>
    public void ShowRewards(List<IItemEffect> rewards, Action<IItemEffect> callback)
    {
        if (rewardPanel == null || rewardButtons == null || rewardButtons.Length == 0)
            return;


        curRewards = rewards;
        onRewardSelectedCallback = callback;

        rewardPanel.SetActive(true);
        
        for (int i = 0; i < rewardButtons.Length; ++i)
        {
            rewardButtons[i].gameObject.SetActive(true);

            int idx = i;    // Ŭ���� �̽� �ذ�� ���� ����
            rewardButtons[i].onClick.RemoveAllListeners();
            rewardButtons[i].onClick.AddListener(() => OnRewardButtonClicked(idx));
        }
    }

    /// <summary>
    /// ��ư Ŭ�� �� ���õ� ������ ����
    /// </summary>
    private void OnRewardButtonClicked(int idx)
    {
        if (curRewards != null && idx < curRewards.Count)
        {
            IItemEffect selectedReward = curRewards[idx];
            rewardPanel.SetActive(false);
            onRewardSelectedCallback?.Invoke(selectedReward);
        }
    }
}
