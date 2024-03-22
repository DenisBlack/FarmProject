using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

public class OrderTooltipFactory 
{
    [Inject] private DiContainer _diContainer;
    private readonly OrderTooltip _productTooltip;
    private readonly Transform _canvasTooltip;
    
    private List<OrderTooltip> _tooltipsList = new List<OrderTooltip>();

    public OrderTooltipFactory(OrderTooltip productTooltip, Transform canvasTooltip)
    {
        _productTooltip = productTooltip;
        _canvasTooltip = canvasTooltip;
    }

    public OrderTooltip CreateOrderTooltip(Order order)
    {
        var tooltip = _diContainer.InstantiatePrefab(_productTooltip, _canvasTooltip).GetComponent<OrderTooltip>();
        tooltip.Initialized(order, null, Release);
        _tooltipsList.Add(tooltip);
        return tooltip;
    }
        
    private void Release(OrderTooltip tooltip)
    {
        Object.Destroy(tooltip.gameObject);
    }
}
