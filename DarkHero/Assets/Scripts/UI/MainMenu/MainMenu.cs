using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _shopPanel;

    public void OnClickShopButton()
    {
        _shopPanel.SetActive(true);
    }
}
