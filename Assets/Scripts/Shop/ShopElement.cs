using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopElement : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _productAmount;
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _priceText;
    
    private Action<ShopProductData> _callback;
    private ShopProductData _shopProductData;

    public void Initialized(ShopProductData shopProductData, Action<ShopProductData> callback)
    {
        _shopProductData = shopProductData;
        _image.sprite = shopProductData.ItemData.ItemIcon;
        _productAmount.text = shopProductData.ItemAmount.ToString();
        _priceText.text = shopProductData.Price.ToString();
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
