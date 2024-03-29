using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;

public abstract class ShopProductData : ScriptableObject
{
    [SerializeField] private int _price;
    public int Price => _price;
    public abstract Sprite Icon { get; }
}

