using System;
using Inventory;
using UnityEngine;
using Utils;

namespace Gardening
{
    public class Product : MonoBehaviour
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
                PickUpAction?.Invoke(this);
        }
    }
}