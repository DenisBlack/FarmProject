using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Inventory;
using Orders;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class OrderSystem : IInitializable
{
    public Order TargetOrder => _targetOrder;
    private Order _targetOrder;

    private GameSettingInstaller.Settings _settings;
    private readonly CoinSystem _coinSystem;
    private readonly InventoryStorage _storage;

    public Action<Order> OrderCreated;
    public Action<Order> OrderProcessed;

    private HashSet<IOrderAgent> _orderAgents = new HashSet<IOrderAgent>();

    public OrderSystem(GameSettingInstaller.Settings settings, CoinSystem coinSystem, InventoryStorage storage)
    {
        _settings = settings;
        _coinSystem = coinSystem;
        _storage = storage;
    }

    public void Initialize()
    {
        CreateOrder();
    }

    private void CreateOrder()
    {
        var container = _settings.OrderContainer;
        var randomOrder = container.OrderDatas[Random.Range(0, container.OrderDatas.Length)];
        _targetOrder = new Order(randomOrder);
        Debug.Log(_targetOrder.OrderInfo().name);
        
        OrderCreated?.Invoke(_targetOrder);
    }

    public void TryCompleteOrder()
    {
        if (CanCompletedOrder())
        {
            ProcessOrder();
            WaitForAgents().Forget();
        }
        else Debug.Log("Cant Completed! " + _targetOrder.OrderInfo().name);
    }

    private void ProcessOrder()
    {
        Debug.Log("Order Completed! " + _targetOrder.OrderInfo().name);
        _coinSystem.AddCoins(_targetOrder.OrderInfo().RewardCoins);
        _storage.RemoveItem(_targetOrder.OrderInfo().ItemData, _targetOrder.OrderInfo().Quantity);
        OrderProcessed?.Invoke(_targetOrder);
        _targetOrder = null;
    }

    async UniTask WaitForAgents()
    {
        var cts = new CancellationTokenSource();
        var tasks = _orderAgents.Select(agent => agent.CompleteOrderAndReturn()).ToList();
        var tCancellationThrow = await UniTask.WhenAll(tasks).SuppressCancellationThrow().AttachExternalCancellation(cts.Token);
        if (!tCancellationThrow)
        {
            CreateOrder();
        }
    }

    private bool CanCompletedOrder()
    {
        var orderInfo = _targetOrder.OrderInfo();
        var storageCount = _storage.GetItemCount(orderInfo.ItemData);
        return orderInfo.Quantity <= storageCount;
    }

    public void AddAgent(IOrderAgent agent)
    {
        _orderAgents.Add(agent);
    }

    public void RemoveAgent(IOrderAgent agent)
    {
        _orderAgents.Remove(agent);
    }
}
