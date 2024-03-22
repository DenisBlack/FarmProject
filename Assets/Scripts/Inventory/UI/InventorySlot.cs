using System;
using Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private TMP_Text _itemCountTMP;
    
    public Item Item;
    private Item _item;

    public void Initialized(Item item, int itemValue)
    {
        _item = item;
            
        _itemImage.sprite = item.Data.ItemIcon;
        _itemImage.enabled = true; 
        _itemCountTMP.text = itemValue.ToString();
    }

    public void SetEmpty()
    {
        _itemImage.enabled = false; 
        _itemCountTMP.text = string.Empty;
    }
}