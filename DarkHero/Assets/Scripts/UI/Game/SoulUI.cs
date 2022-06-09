using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SoulUI : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private TMP_Text _soul;



    private void Start()
    {
        _soul.text = _wallet.Soul.ToString();
        _wallet.SoulChange += ChangeText;
    }

    private void ChangeText()
    {
        _soul.text = _wallet.Soul.ToString();
    }
    private void OnEnable()
    {
        _wallet.SoulChange -= ChangeText;
    }
}
