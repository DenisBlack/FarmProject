namespace Shop
{
    public class ShopItem
    {
        private readonly ShopProductData _data;

        public ShopItem(ShopProductData data)
        {
            _data = data;
        }

        public int ItemPrice()
        {
            return _data.Price;
        }

        public void Purchase()
        {

        }
    }
}