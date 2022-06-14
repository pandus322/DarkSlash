using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _winPanel;

    public void ShowGameOverPanel()
    {
        _gameOverPanel.SetActive(true);
    }
    public void ShowWinPanel()
    {
        _winPanel.SetActive(true);
    }
    public void OnClickPauseButton()
    {
        _pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void OnClickContinueButton()
    {
        _pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void OnClickMainMenuButtonn()
    {
        SceneManager.LoadScene(0);
    }
    public void OnClickRestartButton()
    {
        SceneManager.LoadScene(1);
    }

}
