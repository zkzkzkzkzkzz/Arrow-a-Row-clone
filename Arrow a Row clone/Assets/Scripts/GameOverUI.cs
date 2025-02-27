using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameoverPanel;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        GameManager.Instance.OnGameOver += ShowGameOverUI;
    }


    private void ShowGameOverUI(bool end)
    {
        audioSource.Stop();
        gameoverPanel.SetActive(end);
        Time.timeScale = 0f;
    }

    public void ReturnTitle()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScene");
    }

    public void QuitGame()
    {
        Application.Quit();

        // 에디터 환경에서 플레이 모드를 종료하기 위함
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
