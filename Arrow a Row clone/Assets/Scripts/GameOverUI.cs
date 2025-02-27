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

        // ������ ȯ�濡�� �÷��� ��带 �����ϱ� ����
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
