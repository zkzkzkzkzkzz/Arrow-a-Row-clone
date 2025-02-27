using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
    public event Action<bool> OnGameOver;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ShowGameOverUI();
    }

    private void ShowGameOverUI()
    {
        GameOverUI gameOverUI = GetComponent<GameOverUI>();
        if (gameOverUI != null)
            gameOverUI.ShowGameOverUI(true);
    }

    public void EndGame(bool end = true)
    {
        OnGameOver?.Invoke(end);
    }
}
