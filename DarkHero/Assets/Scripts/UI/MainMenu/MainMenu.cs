using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _shopPanel;

    public void OnClickShopButton()
    {
        _shopPanel.SetActive(true);
    }

    public void OnClickPlayButton()
    {
        SceneManager.LoadScene(1);
    }
}
