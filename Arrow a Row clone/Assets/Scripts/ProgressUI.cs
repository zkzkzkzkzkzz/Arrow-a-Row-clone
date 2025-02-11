using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Slider progressBar;
    [SerializeField] private RectTransform playerIcon;
    //[SerializeField] private Transform bossContainer;
    //[SerializeField] private GameObject bossIconPref;

    [Header("Boss Sprites")]
    //[SerializeField] private Sprite firstBoss;
    //[SerializeField] private Sprite finalBoss;
    //[SerializeField] private Sprite normalBoss;

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
    }

    private void Update()
    {
        UpdateProgress();
    }

    private void UpdateProgress()
    {
        if (mapTileMgr == null)
            return;

        int chapter = mapTileMgr.getPlayerChapter();
        int tileIdx = mapTileMgr.GetPlayerTileIdx();

        float normalizedProgress = ((chapter - 1) * 6 + tileIdx) / (float)((totalChapter - 1) * 6);
        progressBar.value = normalizedProgress;

        Debug.Log("chapter: " + chapter + ", tileIdx: " + tileIdx + ", progressValue: " + progressBar.value);
    }
}
