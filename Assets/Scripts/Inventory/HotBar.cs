using System;
using System.Collections.Generic;
using System.Linq;
using Inventory;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class HotBar : MonoBehaviour, IDisposable
{
    [SerializeField] private List<UISlot> _slots = new List<UISlot>();
    private InventoryStorage _inventoryStorage;

    public event Action<Item> OnItemSelected;

    [Inject]
    public void Construct(InventoryStorage inventoryStorage)
    {
        _inventoryStorage = inventoryStorage;
      
        Init();
    }

    private void Init()
    {
        AddListener();
        UpdateSlots();
    }

    private void AddListener()
    {
        _slots.ForEach(x=> x.Selected += SlotOnSelected);
        _inventoryStorage.OnItemUpdate += InventoryStorageOnOnItemUpdate;
    }

    private void SlotOnSelected(int idx)
    {
        foreach (var slot in _slots)
        {
            slot.SelectState(slot.transform.GetSiblingIndex() == idx);
            if (slot.Item != null && slot.transform.GetSiblingIndex().Equals(idx))
            {
                OnItemSelected?.Invoke(slot.Item);
            }
        }
    }

    private void InventoryStorageOnOnItemUpdate()
    {
        UpdateSlots();
    }

    public void UpdateSlots()
    {
        var sortedInventoryStorage = GetSortedSeedDictionary();
        
        int idx = 0;
        foreach (var item in sortedInventoryStorage)
        {
            _slots[idx].UpdateItem(item.Key, _inventoryStorage.GetItemCount(item.Key.Data));
            idx++;
        }
        
         
        for (; idx < _slots.Count; idx++)
        {
            _slots[idx].SetEmpty();
        }
    }
    
    private Dictionary<Item, int> GetSortedSeedDictionary()
    {
        return _inventoryStorage.Items
            .Where(x=> x.Key.Data.ItemType == ItemType.Seed)
            .OrderBy(pair => pair.Value)
            .ToDictionary(pair => pair.Key, pair => pair.Value);
    }

    public void Dispose()
    {
        _inventoryStorage.OnItemUpdate -= InventoryStorageOnOnItemUpdate;
        _slots.ForEach(x=> x.Selected -= SlotOnSelected);
    }
}