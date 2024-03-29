using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Effects;
using Inventory;
using Orders;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class OrderSystem : IInitializable
{
    private Order _targetOrder;
    public Order TargetOrder => _targetOrder;

    private readonly GameSettingInstaller.Settings _settings;
    private readonly CoinSystem _coinSystem;
    private readonly InventoryStorage _storage;
    private readonly BuffSystem _buffSystem;

    public Action<Order> OrderCreated;
    public Action<Order> OrderProcessed;
    public Action<int> IncreaseRewardAmount;
    private int BuffBonusIncrese;
    
    private HashSet<IOrderAgent> _orderAgents = new HashSet<IOrderAgent>();

    public OrderSystem(GameSettingInstaller.Settings settings, CoinSystem coinSystem, InventoryStorage storage, BuffSystem buffSystem)
    {
        _settings = settings;
        _coinSystem = coinSystem;
        _storage = storage;
        _buffSystem = buffSystem;
        _buffSystem.OnBuffStarted += OnBuffStarted;
        _buffSystem.OnBuffCompleted += OnBuffCompleted;
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
        if (!CanCompletedOrder()) 
            return;
        
        ProcessOrder();
        WaitForAgents().Forget();
    }

    private void ProcessOrder()
    {
        _coinSystem.AddCoins(_targetOrder.OrderInfo().RewardCoins + BuffBonusIncrese);
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

    private void OnBuffStarted(Buff buff)
    {
        if (buff.BuffInfo.Type == BuffData.BuffType.IncreaseReward)
        {
            var increaseRewardBuff = buff.BuffInfo.BuffInfoData as IncreaseRewardBuff;
            if (increaseRewardBuff != null)
            {
                BuffBonusIncrese = increaseRewardBuff.IncreaseRewardAmount;
                IncreaseRewardAmount?.Invoke(BuffBonusIncrese);
            }
        }
    }

    private void OnBuffCompleted(Buff buff)
    {
        if (buff.BuffInfo.Type == BuffData.BuffType.IncreaseReward)
        {
            var increaseRewardBuff = buff.BuffInfo.BuffInfoData as IncreaseRewardBuff;
            if (increaseRewardBuff != null)
            {
                BuffBonusIncrese = 0;
                IncreaseRewardAmount?.Invoke(BuffBonusIncrese);
            }
        }
    }
}
