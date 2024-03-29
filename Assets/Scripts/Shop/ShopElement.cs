using System;
using System.Collections;
using System.Collections.Generic;
using Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopElement : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _productAmount;
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private TMP_Text _durationText;
    [SerializeField] private TMP_Text _buffDescription;
    
    private Action<ShopProductData> _callback;
    private ShopProductData _shopProductData;

    public void Initialized(ShopProductData shopProductData, Action<ShopProductData> callback)
    {
        _shopProductData = shopProductData;
        _image.sprite = shopProductData.Icon;
        _priceText.text = shopProductData.Price.ToString();

        switch (shopProductData)
        {
            case BuffProductData data:
                _durationText.text = $"{data.BuffData.Duration}s";
                _buffDescription.text = $"{data.BuffData.BuffDescription}";
                break;
            case SeedProductData data:
                _productAmount.text = data.ItemAmount.ToString();
                break;
        }

        _callback = callback;
    }

    private void Awake()
    {
        _button.onClick.AddListener(OnPurchasedButtonClick);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnPurchasedButtonClick);
    }

    private void OnPurchasedButtonClick()
    {
        _callback?.Invoke(_shopProductData);
    }
}
