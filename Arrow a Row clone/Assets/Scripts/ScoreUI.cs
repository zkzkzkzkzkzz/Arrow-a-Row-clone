using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    private MapTileMgr mapTileMgr;

    private float curScore = 0;
    private float scoreSpeed = 3;

    private void Start()
    {
        mapTileMgr = FindObjectOfType<MapTileMgr>();
        if (mapTileMgr == null)
            Debug.LogError("ScoreUI에서 MapTileMgr을 찾을 수 없습니다.");

        UpdateScore();
    }

    private void Update()
    {
        scoreSpeed = mapTileMgr.GetTileSpeed();
        curScore += scoreSpeed * Time.deltaTime;

        UpdateScore();
    }

    private void UpdateScore()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + ((int)curScore).ToString();
    }
}
