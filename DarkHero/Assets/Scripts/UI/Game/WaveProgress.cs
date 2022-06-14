using UnityEngine;
using UnityEngine.UI;

public class WaveProgress : MonoBehaviour
{
    private float _waveProgress;
    private float _waveMaxProgress;
    [SerializeField] private Image _waveProgressImage;

    public void Init(float waveMaxProgress)
    {
        _waveMaxProgress = waveMaxProgress;
    }
    private void UpdateProgress()
    {
        var progress = _waveProgress / _waveMaxProgress;
        _waveProgressImage.fillAmount = progress;
    }

    public void ResetWave()
    {
        _waveProgressImage.fillAmount = 0;
        _waveProgress = 0;

    }
    public void SetEnemy(Enemy enemy)
    {
        enemy.Dying += OnEnemyDying;
    }
    private void OnEnemyDying(Enemy enemy)
    {
        enemy.Dying -= OnEnemyDying;
        _waveProgress++;
        UpdateProgress();

    }
}
