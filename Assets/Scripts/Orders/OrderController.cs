using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Orders
{
    public class OrderController 
    {
        private OrderSystem _orderSystem;
        private OrderTooltipFactory _orderTooltipFactory;
        private RectTransform _orderParent;

        private List<OrderTooltip> _tooltips = new List<OrderTooltip>();

        [Inject]
        public void Construct(OrderSystem orderSystem, OrderTooltipFactory orderTooltipFactory)
        {
            _orderSystem = orderSystem;
            _orderTooltipFactory = orderTooltipFactory;
            _orderSystem.OrderCreated += OrderCreated;
            _orderSystem.OrderProcessed += OrderProcessed;
            _orderSystem.IncreaseRewardAmount += IncreaseRewardAmount;
        }

        private void IncreaseRewardAmount(int amount)
        {
             _tooltips.ForEach(x=> x.BuffBonusChanged(amount));
        }

        private void OrderCreated(Order order)
        {
            var tooltip = _orderTooltipFactory.CreateOrderTooltip(order);
            _tooltips.Add(tooltip);
        }

        private void OrderProcessed(Order order)
        {
            var tooltip = _tooltips.FirstOrDefault(x => x.Order.Equals(order));
            if (tooltip != null)
            {
                tooltip.Release();
                _tooltips.Remove(tooltip);
            }
        }
    }
}