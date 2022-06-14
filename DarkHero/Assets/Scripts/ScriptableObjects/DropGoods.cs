using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new DropGoods", menuName = "Shop/DropGoods", order = 51)]
public class DropGoods : Goods
{
    [SerializeField] private GameObject _dropTamplate;
    public GameObject DropTamplate => _dropTamplate;
}
