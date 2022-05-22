using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Waves[] _waves;
    private int _currentEnemyIndex;
    private int _currentWaveIndex;
    private int _enemiesLeftToSpawn;
    private float _randX;
    private float _randY;


    private void Awake()
    {
        _enemiesLeftToSpawn = _waves[0].WaveSettings.Length;

    }

    private void Start()
    {
        LaunchWave();
    }

    private IEnumerator SpawnEnemyInWave()
    {
        _randX = UnityEngine.Random.Range(-8.55f, 8.55f);
        _randY = UnityEngine.Random.Range(-4.60f, 4.60f);
        if (_enemiesLeftToSpawn > 0)
        {
            yield return new WaitForSeconds(_waves[_currentWaveIndex]
                .WaveSettings[_currentEnemyIndex]
                .SpawnDelay);
            Instantiate(_waves[_currentWaveIndex]
                .WaveSettings[_currentEnemyIndex].Enemy,
                new Vector2(_randX,_randY), Quaternion.identity);
            _enemiesLeftToSpawn--;
            _currentEnemyIndex++;
            StartCoroutine(SpawnEnemyInWave());
        }
        else
        {
            StopCoroutine(SpawnEnemyInWave());
        }
    }
    private void Update()
    {
        if (_currentWaveIndex < _waves.Length - 1 && FindObjectsOfType<Enemy>().Length <= 0 && _enemiesLeftToSpawn<=0)
        {
            _currentWaveIndex++;
            _enemiesLeftToSpawn = _waves[_currentWaveIndex].WaveSettings.Length;
            _currentEnemyIndex = 0;
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
    //[SerializeField] private GameObject _neededSpawner;
    //public GameObject NeededSpawner { get => _neededSpawner; }
    [SerializeField] private float _spawnDelay;
    public float SpawnDelay { get => _spawnDelay; }
}
