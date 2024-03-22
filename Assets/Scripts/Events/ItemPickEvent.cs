using Inventory;

namespace Events
{
    public class ItemPickEvent : IEvent
    {
        public Item Item => _item;
        public int Quantity => _quantity;
        private readonly Item _item;
        private readonly int _quantity;


        public ItemPickEvent(Item item,  int quantity)
        {
            _item = item;
            _quantity = quantity;
        }
    }
}