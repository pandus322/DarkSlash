using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "new Wallet", menuName = "Wallet", order = 51)]
public class Wallet : ScriptableObject
{
    [SerializeField] private int _soul;
    public int Soul => _soul;

    public event UnityAction SoulChange;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("Soul"))
            _soul = PlayerPrefs.GetInt("Soul");
        
    }

    public void Pay(int price)
    {
        _soul -= price;
    }

    public void AddSoul()
    {
        _soul++;
        PlayerPrefs.SetInt("Soul", _soul);
        PlayerPrefs.Save();
        SoulChange?.Invoke();

    }
}
