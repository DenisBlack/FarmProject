using System.Collections.Generic;
using Gardening.UI;
using UnityEngine;
using Zenject;

namespace Gardening.Factory
{
    public class ProductTooltipFactory
    {
        [Inject] private DiContainer _diContainer;
        private readonly ProductTooltip _productTooltip;
        private readonly Transform _canvasTooltip;
    
        private List<ProductTooltip> _tooltipsList = new List<ProductTooltip>();

        public ProductTooltipFactory(ProductTooltip productTooltip, Transform canvasTooltip)
        {
            _productTooltip = productTooltip;
            _canvasTooltip = canvasTooltip;
        }

        public ProductTooltip CreateProductTooltip(PlantData plantData, Transform targetTransform)
        {
            var tooltip = _diContainer.InstantiatePrefab(_productTooltip, _canvasTooltip).GetComponent<ProductTooltip>();
            tooltip.Initialized(plantData, targetTransform, Release);
            _tooltipsList.Add(tooltip);
            return tooltip;
        }
        
        private void Release(ProductTooltip tooltip)
        {
            Object.Destroy(tooltip.gameObject);
        }
    }
}