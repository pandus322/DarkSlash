﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    //не работает 1 волна
    [SerializeField] private Waves[] _waves;
    [SerializeField] private Hero _target;
    [SerializeField] private WaveProgress _waveProgress;
    private int _currentEnemyIndex;
    private int _currentWaveIndex;
    private int _enemiesLeftToSpawn;
    private float _randX;
    private float _randY;

    private void Start()
    {
        Debug.Log(_waves[0].WaveSettings);
        _enemiesLeftToSpawn = _waves[0].WaveSettings.Length;
        _waveProgress.Init(_enemiesLeftToSpawn);
        LaunchWave();
    }

    private IEnumerator SpawnEnemyInWave()
    {
        _randX = UnityEngine.Random.Range(-8.55f, 8.55f);
        _randY = UnityEngine.Random.Range(-4.60f, 4.60f);
        if (_enemiesLeftToSpawn > 0)
        {
            yield return new WaitForSeconds(_waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex].SpawnDelay);
            Enemy enemy = Instantiate(_waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex].Enemy, new Vector2(_randX,_randY), Quaternion.identity).GetComponent<Enemy>();
            enemy.Init(_target);
            enemy.Dying+=OnEnemyDying;
            _waveProgress.SetEnemy(enemy);
            _enemiesLeftToSpawn--;
            _currentEnemyIndex++;
            StartCoroutine(SpawnEnemyInWave());
        }
        else
        {
            StopCoroutine(SpawnEnemyInWave());
        }
    }

    private void OnEnemyDying(Enemy enemy)
    {
        enemy.Dying -= OnEnemyDying;

    }

    private void Update()
    {
        enabled = true;
        if (_currentWaveIndex < _waves.Length - 1 && FindObjectsOfType<Enemy>().Length <= 0 && _enemiesLeftToSpawn <= 0)
        {
            _currentWaveIndex++;
            _enemiesLeftToSpawn = _waves[_currentWaveIndex].WaveSettings.Length;
            _currentEnemyIndex = 0;
            _waveProgress.ResetWave();
            _waveProgress.Init(_enemiesLeftToSpawn);
            LaunchWave();
        }
    }

    public void LaunchWave()
    {
        StartCoroutine(SpawnEnemyInWave());
    }
}
[Serializable]
public class Waves 
{
    [SerializeField] private WaveSettings[] _waveSettings;
    public WaveSettings[] WaveSettings { get => _waveSettings; }
}
[Serializable]
public class WaveSettings
{
    [SerializeField] private GameObject _enemy;
    public GameObject Enemy { get => _enemy; }
    [SerializeField] private float _spawnDelay;
    public float SpawnDelay { get => _spawnDelay; }
}
