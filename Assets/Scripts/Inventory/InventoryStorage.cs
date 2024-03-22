using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Events;
using ModestTree.Util;
using UnityEngine;
using Zenject;

namespace Inventory
{
    public class InventoryStorage : IInitializable, IDisposable
    {
        public Dictionary<Item, int> _items = new Dictionary<Item, int>();
        public Dictionary<Item, int> Items => _items;

        public event Action OnItemUpdate;

        private EventBinding<ItemPickEvent> _itemPickedEvent;

        public void Initialize()
        {
            _itemPickedEvent = new EventBinding<ItemPickEvent>(OnItemPicked);
            EventBus<ItemPickEvent>.Register(_itemPickedEvent);
        }

        private void OnItemPicked(ItemPickEvent eEvent)
        {
            if (eEvent.Item != null)
                AddItem(eEvent.Item, eEvent.Quantity);
        }

        public void AddItem(Item item, int quantity)
        {
            if (!_items.TryGetValue(item, out _))
                _items.Add(item, quantity);
            else
                _items[item] += quantity;

            OnItemUpdate?.Invoke();
        }

        public void RemoveItem(ItemData itemData, int quantity)
        {
            var targetItem = _items.FirstOrDefault(x => x.Key.Data.Equals(itemData));
            if (targetItem.Key != null)
            {
                if (_items[targetItem.Key] > 1)
                {
                    _items[targetItem.Key] -= quantity;
                }
                else _items.Remove(targetItem.Key);
            }
            
            OnItemUpdate?.Invoke();
        }
        
        public void GetInventoryInfo()
        {
            foreach (var item in _items)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("Item: ");
                stringBuilder.Append(item.Key.Data.ItemName);
                stringBuilder.Append(" ");
                stringBuilder.Append(item.Value.ToString());
                Debug.LogError(stringBuilder);
            }
        }

        public int GetItemCount(ItemData itemData)
        {
            var targetItem = _items.FirstOrDefault(x => x.Key.Data.Equals(itemData));
            if (targetItem.Key != null)
            {
                return targetItem.Value;
            }

            return 0;
        }

        public void Dispose()
        {
            EventBus<ItemPickEvent>.Deregister(_itemPickedEvent);
        }
    }
}