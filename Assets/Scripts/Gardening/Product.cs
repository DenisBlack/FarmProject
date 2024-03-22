using System;
using Events;
using Inventory;
using UnityEngine;
using Utils;

namespace Gardening
{
    public class Product : MonoBehaviour, ICollectible
    {
        public Item Item => _item;
        private Item _item;
        
        public event Action<Product> PickUpAction;

        public void Initialized(Item item, Action<Product> callback)
        {
            _item = item;
            PickUpAction = callback;
        }
        
        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Constants.PlayerTag))
            {
                EventBus<ItemPickEvent>.Raise(new ItemPickEvent(_item, 1));

                PickUpAction?.Invoke(this);
            }
        }

        public void Collect()
        {
            EventBus<ItemPickEvent>.Raise(new ItemPickEvent(_item, 1));
            PickUpAction?.Invoke(this);
        }
    }
}