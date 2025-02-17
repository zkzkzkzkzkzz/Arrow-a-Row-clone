using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Slider progressBar;
    [SerializeField] private RectTransform playerIcon;
    [SerializeField] private List<Image> bossIcons;

    [SerializeField] private List<Vector2> bossIconPos = new List<Vector2>();

    private int totalChapter = 14;
    private MapTileMgr mapTileMgr;

    private void Start()
    {
        progressBar = GetComponent<Slider>();
        if (progressBar == null)
            Debug.LogError("ProgressUI���� Slider�� ã�� �� �����ϴ�.");

        mapTileMgr = FindObjectOfType<MapTileMgr>();
        if (mapTileMgr == null)
            Debug.LogError("ProgressUI���� MapTileMgr�� ã�� �� �����ϴ�.");

        UpdateProgress();

        CalculateBossIconPos();
        UpdateBossIconPos();
    }

    private void Update()
    {
        UpdateProgress();
        CheckBossIcon();
    }

    private void UpdateProgress()
    {
        if (mapTileMgr == null)
            return;

        int chapter = mapTileMgr.getPlayerChapter();
        int tileIdx = mapTileMgr.GetPlayerTileIdx();

        float normalizedProgress = ((chapter - 1) * 6 + tileIdx) / (float)((totalChapter - 1) * 6);
        progressBar.value = normalizedProgress;
    }

    private void CalculateBossIconPos()
    {
        List<(int chapter, int tileIdx)> bossTiles = new List<(int, int)>
        {
            (1, 5), // ù ��° ����
            (4, 5),
            (7, 5),
            (10, 5),
            (14, 5) // ���� ����
        };

        for (int i = 0; i < bossTiles.Count; ++i)
        {
            int chapter = bossTiles[i].chapter;
            int tileIdx = bossTiles[i].tileIdx;

            float normalizedProgress = Mathf.Clamp(((chapter - 1) * 6 + tileIdx) / (float)((totalChapter - 1) * 6), 0f, 1f);
            bossIconPos.Add(new Vector2(0, normalizedProgress));
        }
    }

    private void UpdateBossIconPos()
    {
        RectTransform handleArea = progressBar.handleRect.parent.GetComponent<RectTransform>();
        float handleWidth = handleArea.rect.width;

        for (int i = 0; i < bossIcons.Count; ++i)
        {
            float normalizedValue = bossIconPos[i].y;

            float targetX = Mathf.Lerp(-handleWidth / 2f, handleWidth / 2f, normalizedValue);

            bossIcons[i].rectTransform.anchoredPosition = new Vector2(targetX, bossIcons[i].rectTransform.anchoredPosition.y);
        }
    }

    /// <summary>
    /// �÷��̾ �ش� ������ ��Ҵ��� �Ǻ��Ͽ� �������� ����
    /// </summary>
    private void CheckBossIcon()
    {
        float curProgress = progressBar.value;

        for (int i = 0; i < bossIcons.Count; ++i)
        {
            if (curProgress > bossIconPos[i].y)
                bossIcons[i].color = Color.gray;
        }
    }
}
