using System;
using System.Collections;
using System.Collections.Generic;
using Gardening;
using Inventory;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ProductReference", menuName = "ProductReference")]
public class ProductReference : ScriptableObject
{
    [SerializeField] private ItemData _itemData;
    public ItemData GetAsset() => _itemData;
}
