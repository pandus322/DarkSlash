using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Shop : MonoBehaviour
{
    [SerializeField] private List<Goods> _goods;
    [SerializeField] private ItemContainer _tamplate;
    [SerializeField] private GameObject _itemContainer;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private TMP_Text _soul;
    [SerializeField] GameObject _infoPanel;

    private void Start()
    {
        UpdateBalance();
        foreach (var good in _goods)
        {
            AddItem(good);
        }
    }
    private void UpdateBalance()
    {
        _soul.text= _wallet.Soul.ToString();
    }
    private void AddItem(Goods good)
    {
        var view = Instantiate(_tamplate, _itemContainer.transform);
        view.SellButtonClick += OnSellButtonClick;
        view.Render(good);
    }

    private void OnSellButtonClick(Goods good, ItemContainer itemContainer)
    {
        TrySellGood(good, itemContainer);
    }

    private void TrySellGood(Goods good, ItemContainer itemContainer)
    {
        if(good.Price <= _wallet.Soul)
        {
            _wallet.Pay(good.Price);
            good.Upgrade();
            itemContainer.Render(good);
            UpdateBalance();
            itemContainer.SellButtonClick -= OnSellButtonClick;
        }
        else
        {
            _infoPanel.SetActive(true);
        }
    }

    public void OnClickExit()
    {
        gameObject.SetActive(false);
    }
}
