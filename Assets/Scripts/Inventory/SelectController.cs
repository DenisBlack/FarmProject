using UnityEngine;

namespace Inventory
{
    public class SelectController
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
            Debug.Log("SelectedItem: " + SelectedItem.Data.ItemName);
        }
    }
}