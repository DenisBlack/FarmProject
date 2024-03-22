using System.Collections;
using System.Collections.Generic;
using Inventory;
using Popups;
using UnityEngine;
using Zenject;

public class ShopPopup : PopupBase
{
   [SerializeField] private Transform _contentRoot;
   
   private InventoryStorage _inventoryStorage;
   private CoinSystem _coinSystem;
   private GameSettingInstaller.Settings _settings;

   private List<ShopElement> ShopElements = new List<ShopElement>();

   [Inject]
   private DiContainer _diContainer;
   
   [Inject]
   public void Construct(InventoryStorage inventoryStorage, CoinSystem coinSystem, GameSettingInstaller.Settings settings)
   {
      _inventoryStorage = inventoryStorage;
      _coinSystem = coinSystem;
      _settings = settings;
   }

   public void InitShop()
   {
      var productContainer = _settings.ShopProductContainer;
      for (int i = 0; i < productContainer.ShopProductDatas.Length; i++)
      {
         var shopElement = _diContainer.InstantiatePrefab(_settings.ShopElement, _contentRoot).GetComponent<ShopElement>();
         shopElement.Initialized(productContainer.ShopProductDatas[i], OnPurchased);
         ShopElements.Add(shopElement);
      }
   }

   private void OnPurchased(ShopProductData shopProductData)
   {
      if (_coinSystem.CanSpendCoins(shopProductData.Price))
      {
         _coinSystem.ReduceCoins(shopProductData.Price);
         _inventoryStorage.AddItem(new Item(shopProductData.ItemData), shopProductData.ItemAmount);
      }
   }
   
   public override void Shown()
   {
      base.Shown();
      
      InitShop();
   }

   public override void Hide()
   {
      base.Hide();
      
      ShopElements.ForEach(x=> Destroy(x.gameObject));
   }
}
