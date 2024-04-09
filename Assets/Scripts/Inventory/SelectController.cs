using System;
using UnityEngine;

namespace Inventory
{
    public class SelectController : IDisposable
    {
        private HotBar _hotBar;

        public Item SelectedItem { get; private set; }

        public SelectController(HotBar hotBar)
        {
            _hotBar = hotBar;
            Init();
        }

        private void Init()
        {
            _hotBar.OnItemSelected += HotBarOnOnItemSelected;
        }

        private void HotBarOnOnItemSelected(Item item)
        {
            SelectedItem = item;
            if(item != null)
                Debug.Log("SelectedItem: " + SelectedItem.Data.ItemName);
            else
                Debug.Log("SelectedItem null! ");
        }
        
        public void Dispose()
        {
            _hotBar.OnItemSelected -= HotBarOnOnItemSelected;
        }
    }
}