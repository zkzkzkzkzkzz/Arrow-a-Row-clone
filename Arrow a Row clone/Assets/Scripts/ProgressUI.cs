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
            Debug.LogError("ProgressUI에서 Slider를 찾을 수 없습니다.");

        mapTileMgr = FindObjectOfType<MapTileMgr>();
        if (mapTileMgr == null)
            Debug.LogError("ProgressUI에서 MapTileMgr을 찾을 수 없습니다.");

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
            (1, 5), // 첫 번째 보스
            (4, 5),
            (7, 5),
            (10, 5),
            (14, 5) // 최종 보스
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
    /// 플레이어가 해당 보스를 잡았는지 판별하여 색상으로 구분
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
