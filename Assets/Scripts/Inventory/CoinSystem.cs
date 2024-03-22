using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;
using Zenject;

public class CoinSystem : IInitializable
{
    private int _coins = 100;

    public Action<int> CoinsChangedAmount;

    public void Initialize()
    {
        CoinsChangedAmount?.Invoke(_coins);
    }

    public void AddCoins(int amount)
    {
        _coins += amount;
        UpdateListeners();
    }

    public void ReduceCoins(int amount)
    {
        if (amount > _coins)
            _coins = 0;
        else
            _coins -= amount;

        UpdateListeners();
    }

    public bool CanSpendCoins(int amount)
    {
        return _coins >= amount;
    }

    private void UpdateListeners()
    {
        CoinsChangedAmount?.Invoke(_coins);
    }
}
