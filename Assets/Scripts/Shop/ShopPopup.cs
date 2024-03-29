using System.Collections;
using System.Collections.Generic;
using Buffs;
using Effects;
using Inventory;
using Popups;
using Shop;
using UnityEngine;
using Zenject;

public class ShopPopup : PopupBase
{
   [SerializeField] private Transform _contentRoot;

   [Inject]
   private DiContainer _diContainer;
   private InventoryStorage _inventoryStorage;
   private CoinSystem _coinSystem;
   private GameSettingInstaller.Settings _settings;
   private BuffSystem _buffSystem;
   private BuffFactory _buffFactory;

   private List<ShopElement> ShopElements = new List<ShopElement>();

   [Inject]
   public void Construct(InventoryStorage inventoryStorage, CoinSystem coinSystem, GameSettingInstaller.Settings settings, BuffSystem buffSystem, BuffFactory buffFactory)
   {
      _inventoryStorage = inventoryStorage;
      _coinSystem = coinSystem;
      _settings = settings;
      _buffSystem = buffSystem;
      _buffFactory = buffFactory;
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
         
         switch (shopProductData)
         {
            case BuffProductData data:
               _buffSystem.AddEffect(_buffFactory.CreateBuff(data.BuffData));
               break;
            case SeedProductData data:
               _inventoryStorage.AddItem(new Item(data.ItemData), data.ItemAmount);
               break;
         }
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
