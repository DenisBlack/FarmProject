using Inventory;
using UnityEngine;
using Zenject;

namespace Gardening.Factory
{
    public class ProductFactory
    {
        [Inject] private DiContainer _diContainer;
        public Product CreateProduct(Item item, Product prefab, Transform parent)
        {
            var product = _diContainer.InstantiatePrefab(prefab, parent).GetComponent<Product>();
            product.Initialized(item, Release);
            return product;
        }

        private void Release(Product product)
        {
            Object.Destroy(product.gameObject);
        }
    }
}