using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    [SerializeField] private Goods _heart;
    [SerializeField] private GameObject _tamplate;
    [SerializeField] private GameObject _itemContainer;
    [SerializeField] private Hero _hero;
    private List<Image> _heartList = new List<Image>();
    private void Start()
    {
        Debug.Log(_heart.Level);
        for (int i = 0; i <= _heart.Level; i++)
        {
            _heartList.Add(Instantiate(_tamplate, _itemContainer.transform).GetComponent<Image>());
        }
    }


    public void RemooveHeart()
    {
        Destroy(_heartList[_heartList.Count-1]);
        _heartList.RemoveAt(_heartList.Count-1);
    }
}
