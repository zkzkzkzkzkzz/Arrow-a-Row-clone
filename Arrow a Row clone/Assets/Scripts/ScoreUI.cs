using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TextMeshProUGUI coinText;

    private MapTileMgr mapTileMgr;

    private float curScore = 0;
    private float scoreSpeed;

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

        coinText.text = ShopManager.Instance.GetPlayerCoins().ToString();
    }

    public void AddCoin()
    {
        int coin = ShopManager.Instance.GetPlayerCoins();
        coin += Mathf.RoundToInt(curScore * 0.026f);
        ShopManager.Instance.SetPlayerCoins(coin);
    }
}
