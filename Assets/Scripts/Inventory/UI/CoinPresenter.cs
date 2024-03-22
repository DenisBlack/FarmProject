using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class CoinPresenter : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinTMP;
    private CoinSystem _coinSystem;

    [Inject]
    public void Construct(CoinSystem coinSystem)
    {
        _coinSystem = coinSystem;
    }

    private void OnEnable()
    {
        _coinSystem.CoinsChangedAmount += CoinsChangedAmount;
    }

    private void CoinsChangedAmount(int coinAmount)
    {
        _coinTMP.text = coinAmount.ToString();
    }

    private void OnDestroy()
    {
        _coinSystem.CoinsChangedAmount -= CoinsChangedAmount;
    }
}
