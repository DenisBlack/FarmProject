using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopProductData", menuName = "Shop/Shop Product Data")]
public class ShopProductData : ScriptableObject
{
    [Header("Item")]
    [SerializeField] private ItemData _itemData;
    [SerializeField] private int _itemAmount;
    [Header("Price")] 
    [SerializeField] private int _price;

    public ItemData ItemData => _itemData;
    public int ItemAmount => _itemAmount;
    public int Price => _price;
}
