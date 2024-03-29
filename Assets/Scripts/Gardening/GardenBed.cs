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
        private GardenTimerTooltip _timerTooltip;
        private GardenSystem _gardenSystem;
        private HarvestController _harvestController;

        [Inject]
        public void Construct(GrowingTimerFactory growingTimerFactory, ProductTooltipFactory productTooltipFactory, GardenSystem gardenSystem)
        {
            _growingTimerFactory = growingTimerFactory;
            _productTooltipFactory = productTooltipFactory;
            _gardenSystem = gardenSystem;
        }

        private void Start()
        {
            _gardenSystem.ReduceGrowTimerAction += ReduceGrowTimerAction;
        }

        private void ReduceGrowTimerAction(int percent)
        {
            if (_isFree)
              return;

            if (_timerTooltip != null)
                _timerTooltip.GrowingBuffChanges(percent);
            
            if(_harvestController != null)
                _harvestController.BuffChanged(percent);
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

                if (_gardenSystem.BuffActivated)
                {
                    ReduceGrowTimerAction(_gardenSystem.BuffPercent);
                }
            }
            
            result?.Invoke(false);
        }

        public void InitHarvestController(PlantData plantData)
        {
            _harvestController = _diContainer.InstantiatePrefab(plantData.HarvestController, Pivot).GetComponent<HarvestController>();
            _harvestController.Initialized(plantData, ProductGrownCompleted, ProductHasPicked);
        }

        private void CreateGrowTimer(PlantData plantData)
        {
           _timerTooltip = _growingTimerFactory.CreateGardenTimerTooltip(plantData, Pivot);
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

        private void OnDestroy()
        {
            _gardenSystem.ReduceGrowTimerAction -= ReduceGrowTimerAction;
        }
    }
}