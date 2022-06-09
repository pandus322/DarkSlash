using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] private GameObject _infoPanel;

    public void OnClickExit()
    {
        _infoPanel.SetActive(false);
    }
}
