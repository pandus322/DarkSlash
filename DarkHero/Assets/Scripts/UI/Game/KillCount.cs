using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillCount : MonoBehaviour
{
    [SerializeField] private TMP_Text _killText;
    private int _killCount;

    public void AddKill()
    {
        _killCount++;
        UpdateText();
    }
    private void UpdateText()
    {
        _killText.text = _killCount.ToString();
    }

}
