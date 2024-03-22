using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Gardening.UI
{
    public class ProductTooltip : Tooltip
    {
        [SerializeField] private Image _productIcon;
        
        private Action<ProductTooltip> _callback;
        public void Initialized(PlantData plantData, Transform targetTransform, Action<ProductTooltip> callback)
        {
            _productIcon.sprite = plantData.ProductReference.GetAsset().ItemIcon;
            _callback = callback;
            AttachToTransform(targetTransform , 1.5f);
        }

        public void Release()
        {
            _callback?.Invoke(this);
        }
    }
}