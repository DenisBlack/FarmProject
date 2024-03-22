namespace Inventory
{
    public class Item
    {
        private readonly ItemData _data;
        public ItemData Data => _data;

        public Item(ItemData data)
        {
            _data = data;
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Item otherItem = (Item)obj;
            return _data.Equals(otherItem._data);
        }

        public override int GetHashCode()
        {
            return _data.GetHashCode();
        }
    }
}