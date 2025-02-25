using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();

        // 에디터 환경에서 플레이 모드를 종료하기 위함
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

}
