using System;
using System.Collections;
using System.Collections.Generic;
using Gardening.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderTooltip : Tooltip
{
   [SerializeField] private TMP_Text _orderTMP;
   [SerializeField] private Image _orderIcon;
   [SerializeField] private TMP_Text _orderAmount;
   private Action<OrderTooltip> _callback;
   private OrderData _orderData;
   private Order _order;

   public Order Order => _order;

   public void Initialized(Order order, Transform targetTransform, Action<OrderTooltip> callback)
   {
       _order = order;
       _orderData = order.OrderInfo();
       _orderTMP.text =  _orderData.ItemData.ItemName;
       _orderIcon.sprite =  _orderData.ItemData.ItemIcon;
       _orderAmount.text =  _orderData.Quantity.ToString();

       _callback = callback;
       
       //AttachToTransform(targetTransform, 0.9f);
   }
   
   public void Release()
   {
       _callback?.Invoke(this);
   }
}
