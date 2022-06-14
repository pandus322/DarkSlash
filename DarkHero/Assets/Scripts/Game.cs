using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private WaveSpawner _waveSpawner;
    [SerializeField] private Hero _hero;
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private List<Goods> _goods;
    private void Start()
    {
        Time.timeScale = 1;
        _waveSpawner.EndWave += WinGame;
        _hero.Dying += GameOver;
        _hero.Init(_goods[0].Level, _goods[1].Level, _goods[2].Level);
    }
    private void GameOver()
    {
        _hero.Dying -= GameOver;
        Time.timeScale = 0;
        _gameUI.ShowGameOverPanel();
    }
    private void WinGame()
    {
        _waveSpawner.EndWave -= WinGame;
        Time.timeScale = 0;
        _gameUI.ShowWinPanel();
    }
}
