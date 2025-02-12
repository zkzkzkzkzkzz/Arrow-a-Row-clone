using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    private float curScore = 0;
    [SerializeField] private int scoreSpeed = 3;

    private void Start()
    {
        UpdateScore();
    }

    private void Update()
    {
        curScore += scoreSpeed * Time.deltaTime;

        UpdateScore();
    }

    private void UpdateScore()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + ((int)curScore).ToString();
    }
}
