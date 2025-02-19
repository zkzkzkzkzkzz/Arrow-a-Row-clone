using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RewardUI : MonoBehaviour
{
    [SerializeField] private GameObject rewardPanel;
    [SerializeField] private Button[] rewardButtons;

    // 보상 선택 시 호출할 콜백
    private Action<IItemEffect> onRewardSelectedCallback;

    // 현재 표시 중인 보상 목록
    private List<IItemEffect> curRewards;


    /// <summary>
    /// 보상 목록과 선택 시 호출할 콜백을 받아 패널 활성화
    /// </summary>
    public void ShowRewards(List<IItemEffect> rewards, Action<IItemEffect> callback)
    {
        if (rewardPanel == null || rewardButtons == null || rewardButtons.Length == 0)
            return;

        Time.timeScale = 0f;

        curRewards = rewards;
        onRewardSelectedCallback = callback;

        rewardPanel.SetActive(true);
        
        for (int i = 0; i < rewardButtons.Length; ++i)
        {
            rewardButtons[i].gameObject.SetActive(true);

            int idx = i;    // 클로저 이슈 해결용 지역 변수
            rewardButtons[i].onClick.RemoveAllListeners();
            rewardButtons[i].onClick.AddListener(() => OnRewardButtonClicked(idx));

            string str = rewards[i].GetEffectName();
            rewardButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = str + rewards[i].Level.ToString();
        }
    }

    /// <summary>
    /// 버튼 클릭 시 선택된 보상을 전달
    /// </summary>
    private void OnRewardButtonClicked(int idx)
    {
        if (curRewards != null && idx < curRewards.Count)
        {
            IItemEffect selectedReward = curRewards[idx];
            rewardPanel.SetActive(false);

            Time.timeScale = 1f;

            onRewardSelectedCallback?.Invoke(selectedReward);
        }
    }
}
