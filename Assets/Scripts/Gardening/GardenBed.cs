using System;
using Gardening.Factory;
using Gardening.UI;
using UnityEngine;
using Zenject;

namespace Gardening
{
    public class GardenBed : MonoBehaviour
    {
        [SerializeField] private Transform[] _points;
        [SerializeField] private Transform pivot;
        public Transform Pivot => pivot;
        
        private GrowingTimerFactory _growingTimerFactory;
        private ProductTooltipFactory _productTooltipFactory;

        private bool _isFree = true;

        [Inject] private DiContainer _diContainer;
        private PlantData _plantData;
        private ProductTooltip _productTooltip;

        [Inject]
        public void Construct(GrowingTimerFactory growingTimerFactory, ProductTooltipFactory productTooltipFactory)
        {
            _growingTimerFactory = growingTimerFactory;
            _productTooltipFactory = productTooltipFactory;
        }
        
        public void TryPlantSeed(PlantData plantData, Action<bool> result)
        {
            if (_isFree)
            {
                result?.Invoke(true);
                _isFree = false;

                _plantData = plantData;
                InitHarvestController(_plantData);
                CreateGrowTimer(_plantData);
            }
            
            result?.Invoke(false);
        }
        
        public void InitHarvestController(PlantData plantData)
        {
            var harvestController = _diContainer.InstantiatePrefab(plantData.HarvestController, Pivot).GetComponent<HarvestController>();
            harvestController.Initialized(plantData, ProductGrownCompleted, ProductHasPicked);
      
            CreateGrowTimer(plantData);
        }

        private void CreateGrowTimer(PlantData plantData)
        {
            _growingTimerFactory.CreateGardenTimerTooltip(plantData, Pivot);
        }

        private void ProductGrownCompleted()
        {
            _productTooltip = _productTooltipFactory.CreateProductTooltip(_plantData, Pivot.transform);
        }

        private void ProductHasPicked()
        {
            if (_productTooltip == null)
                return;
            
            _productTooltip.Release();
            
            ClearBed();
        }

        private void ClearBed()
        {
            _isFree = true;
            _plantData = null;
        }
    }
}