using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Inventory;
using Popups;
using UnityEngine;
using Zenject;

public class InventoryPopup : PopupBase
{
    [SerializeField] private InventorySlot[] _slots;
    
    private InventoryStorage _inventoryStorage;

    [Inject]
    public void Construct(InventoryStorage inventoryStorage)
    {
        _inventoryStorage = inventoryStorage;
    }
    
    public void OnStorageUpdate()
    {
        UpdateInventory();
    }

    private void UpdateInventory()
    {
        var sortedInventoryStorage = GetSortedProductDictionary();
        
        int idx = 0;
        foreach (var item in sortedInventoryStorage)
        {
            _slots[idx].Initialized(item.Key, item.Value);
            idx++;
        }
        
        for (; idx < _slots.Length; idx++)
        {
            _slots[idx].SetEmpty();
        }
    }


    public override void Shown()
    {
        base.Shown();
        _inventoryStorage.OnItemUpdate += OnStorageUpdate;
        UpdateInventory();
    }

    public override void Hide()
    {
        base.Hide();
        _inventoryStorage.OnItemUpdate -= OnStorageUpdate;
    }
    
    private Dictionary<Item, int> GetSortedProductDictionary()
    {
        return _inventoryStorage.Items
            .Where(x=> x.Key.Data.ItemType == ItemType.Product)
            .OrderBy(pair => pair.Value)
            .ToDictionary(pair => pair.Key, pair => pair.Value);
    }
}