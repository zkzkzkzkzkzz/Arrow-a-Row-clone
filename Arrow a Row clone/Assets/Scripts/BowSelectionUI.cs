using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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


            string str = curBows[i].bowName;
            bowButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = str;
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
}
