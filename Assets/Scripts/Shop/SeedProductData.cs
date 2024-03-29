using Inventory;
using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(fileName = "SeedProductData", menuName = "Shop/Seed Product Data")]
    public class SeedProductData : ShopProductData
    {
        [Header("Item")]
        [SerializeField] private ItemData _itemData;
        [SerializeField] private int _itemAmount;
    
        public ItemData ItemData => _itemData;
        public int ItemAmount => _itemAmount;
        
        public override Sprite Icon => _itemData.ItemIcon;
    }
}