using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BowSelectionUI : MonoBehaviour
{
    [SerializeField] private GameObject bowPanel;
    [SerializeField] private Button[] bowButtons;

    private Action<BowSO> onBowSelectionCallback;
    private List<BowSO> curBows;

    [SerializeField] private List<BowSO> availableBows;

    public void ShowBows(Action<BowSO> callback)
    {
        if (bowPanel == null || bowButtons == null || bowButtons.Length == 0)
            return;

        Time.timeScale = 0f;

        onBowSelectionCallback = callback;
        bowPanel.SetActive(true);

        curBows = new List<BowSO>(availableBows);

        for (int i = 0; i < bowButtons.Length; ++i)
        {
            bowButtons[i].gameObject.SetActive(true);
            int idx = i;
            bowButtons[i].onClick.RemoveAllListeners();
            bowButtons[i].onClick.AddListener(() => onBowButtonClicked(idx));

            bowButtons[i].GetComponent<Image>().sprite = curBows[i].sprite;
            bowButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = BuildBonusDescription(curBows[i]);
        }
    }

    private void onBowButtonClicked(int idx)
    {
        if (curBows != null && idx < curBows.Count)
        {
            BowSO selectedBow = curBows[idx];
            bowPanel.SetActive(false);
            Time.timeScale = 1f;
            onBowSelectionCallback?.Invoke(selectedBow);
        }
    }

    /// <summary>
    /// BowSO의 StatBonusList에서 1레벨의 스탯 보너스 정보를 문자열로 반환
    /// </summary>
    /// <param name="bow"></param>
    /// <returns></returns>
    private string BuildBonusDescription(BowSO bow)
    {
        var bonusList = bow.StatBonusList[0];
        StringBuilder sb = new StringBuilder();
        foreach (var bonus in bonusList)
        {
            if (bonus.statType == StatType.PERCENTAGE)
                sb.AppendFormat("{0} : {1}%\n", bonus.statType.GetStatName(), bonus.value);
            else
            {
                if (bonus.value >= 0)
                    sb.AppendFormat("{0} +{1}\n", bonus.statType.GetStatName(), bonus.value);
                else
                    sb.AppendFormat("{0} {1}\n", bonus.statType.GetStatName(), bonus.value);
            }
        }

        return sb.ToString();
    }
}
