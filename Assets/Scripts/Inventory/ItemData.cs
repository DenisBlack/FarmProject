using UnityEngine;
using UnityEngine.Serialization;

namespace Inventory
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Inventory/ItemData")]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private string _itemName;
        [SerializeField] private Sprite _itemIcon;
        [SerializeField] private ItemType _itemType; 
        public string ItemName => _itemName;
        public Sprite ItemIcon => _itemIcon;
        public ItemType ItemType => _itemType; 
    }
    
    public enum ItemType
    {
        Product,
        Seed,
        Coin
    }
}
