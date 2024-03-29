using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Gardening.Factory;
using Inventory;
using UnityEngine;
using Zenject;

namespace Gardening
{
    public class HarvestController : MonoBehaviour
    {
        [SerializeField] private GameObject[] _productPoints;
    
        private PlantFactory _plantFactory;
        private ProductFactory _productFactory;
        private PlantData _plantData;
        private List<Plant> _plants = new List<Plant>();
        private List<Product> _products = new List<Product>();
        private InventoryStorage _inventoryStorage;

        public event Action ProductsHasGrown;
        public event Action ProductsHasPicked;
    
        [Inject]
        public void Construct(PlantFactory plantFactory, ProductFactory productFactory, InventoryStorage inventoryStorage)
        {
            _plantFactory = plantFactory;
            _productFactory = productFactory;
            _inventoryStorage = inventoryStorage;
        }
    
        public void Initialized(PlantData plantData, Action callback, Action callback2)
        {
            _plantData = plantData;
            ProductsHasGrown = callback;
            ProductsHasPicked = callback2;
            SpawnPlants(plantData);
            WaitUntilAllPlantsGrownAndCollectedAsync().Forget();
        }

        private void SpawnPlants(PlantData plantData)
        {
            for (int i = 0; i < _productPoints.Length; i++)
            {
                var plant = _plantFactory.Create(plantData, _productPoints[i].transform);
                _plants.Add(plant);
            }
        }

        async UniTaskVoid WaitUntilAllPlantsGrownAndCollectedAsync()
        {
            await UniTask.WaitUntil(AllPlantsGrownCompleted);
            CreateProducts();
            ProductsHasGrown?.Invoke();
            await UniTask.WaitUntil(AllProductsCollected);
            ProductsHasPicked?.Invoke();
        }

        private void CreateProducts()
        {
            for (int i = 0; i < _productPoints.Length; i++)
            {
                var product = _productFactory.CreateProduct(new Item(_plantData.ProductReference.GetAsset()), _plantData.ProductPrefab, _productPoints[i].transform);
                product.PickUpAction += delegate(Product concreteProduct)
                {
                    OnPickedProduct(product.Item);
                    _products.Remove(concreteProduct);
                };
                
                _products.Add(product);
            }
        }

        private void OnPickedProduct(Item item)
        {
            _inventoryStorage.AddItem(item, 1);
        }

        private bool AllPlantsGrownCompleted()
        {
            return _plants.All(x => x.IsPlantGrew);
        }
    
        private bool AllProductsCollected()
        {
            return _products.Count == 0;
        }

        public void BuffChanged(int percent)
        {
            if (_plants.All(x => x != null))
            {
                _plants.ForEach(x=> x.GrowingBuffChanges(percent));
            }
        }
    }
}
