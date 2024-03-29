using Effects;
using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(fileName = "BuffProductData", menuName = "Shop/Buff Product Data")]
    public class BuffProductData : ShopProductData
    {
        [SerializeField] private BuffData _buffData;
        public BuffData BuffData => _buffData;

        public override Sprite Icon => _buffData.Icon;
    }
}