using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Gardening.Factory
{
    public class PlantFactory
    {
        [Inject] private DiContainer _diContainer;
        private List<Plant> _gardenPlants = new List<Plant>();
        
        public Plant Create(PlantData plantData, Transform parentTransform)
        {
            var gardenPlant = _diContainer.InstantiatePrefab(plantData.PlantPrefab, parentTransform).GetComponent<Plant>();
            gardenPlant.Initialized(plantData, Release);
            _gardenPlants.Add(gardenPlant);
            return gardenPlant;
        }
        
        public void Release(Plant plant)
        {
            _gardenPlants.Remove(plant);
            Object.Destroy(plant.gameObject);
        }
    }
}