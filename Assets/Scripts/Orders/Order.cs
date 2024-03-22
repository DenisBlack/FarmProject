using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order 
{
    private OrderData _data;
    public Order(OrderData data)
    {
        _data = data;
    }

    public OrderData OrderInfo()
    {
        return _data;
    }
}
