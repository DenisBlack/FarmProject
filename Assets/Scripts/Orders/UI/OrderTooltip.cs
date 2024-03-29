using System;
using System.Collections;
using System.Collections.Generic;
using Gardening.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class OrderTooltip : Tooltip
{
   [SerializeField] private TMP_Text _orderTMP;
   [SerializeField] private Image _orderIcon;
   [SerializeField] private TMP_Text _orderAmountTMP;
   [SerializeField] private TMP_Text _rewardAmountTMP;
   private Action<OrderTooltip> _callback;
   private OrderData _orderData;
   private Order _order;

   public Order Order => _order;

   public void Initialized(Order order, Transform targetTransform, Action<OrderTooltip> callback)
   {
       _order = order;
       _orderData = order.OrderInfo();
       _callback = callback;
       
       UpdateOrderInformation();
   }

   private void UpdateOrderInformation()
   {
       _orderTMP.text = _orderData.ItemData.ItemName;
       _orderIcon.sprite =  _orderData.ItemData.ItemIcon;
       _orderAmountTMP.text =  _orderData.Quantity.ToString();
       _rewardAmountTMP.text = _orderData.RewardCoins.ToString();
   }

   public void BuffBonusChanged(int value)
   {
       _rewardAmountTMP.color = value > 0 ? Color.green : Color.white;
       _rewardAmountTMP.text = (_orderData.RewardCoins + value).ToString();
   }
   
   public void Release()
   {
       _callback?.Invoke(this);
   }
}
