using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "new Good", menuName = "Goods", order =51)]
public abstract class Goods : ScriptableObject
{
    [SerializeField] private string _label;
    [SerializeField] private string _desription;
    [SerializeField] private int _price;
    [SerializeField] private int _level;
    [SerializeField] private Sprite _image;

    public string Label => _label;
    public string Description => _desription;
    public int Price => _price;
    public int Level => _level;
    public Sprite Image => _image;

    private void Awake()
    {
        if (PlayerPrefs.HasKey(_label))
            Init();
    }

    private void Init()
    {
        _level = PlayerPrefs.GetInt(_label);
        for (int i = 0; i < _level; i++)
        {
            _price *= 2;
        }
    }
    public void Upgrade()
    {
        _level++;
        PlayerPrefs.SetInt(_label, _level);
        PlayerPrefs.Save();
        _price *= 2;
    }
}
