using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(fileName = "ShopProductContainer", menuName = "Shop/Shop Product Container")]
    public class ShopProductContainer : ScriptableObject
    {
        [SerializeField] private ShopProductData[] _shopProductDatas;
        public ShopProductData[] ShopProductDatas => _shopProductDatas;
    }
}