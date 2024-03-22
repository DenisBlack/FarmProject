using UnityEngine;

namespace Orders
{
    [CreateAssetMenu(fileName = "OrderContainer", menuName = "Orders/Order Container")]
    public class OrderContainer : ScriptableObject
    {
        [SerializeField] private OrderData[] _orderDatas;
        public OrderData[] OrderDatas => _orderDatas;
    }
}