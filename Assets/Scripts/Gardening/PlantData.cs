using Inventory;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gardening
{
    [CreateAssetMenu(fileName = "Plant", menuName = "Garden/Plant Data")]
    public class PlantData : ScriptableObject
    {
        [SerializeField] private ProductReference _productReference;
        [SerializeField] private float _growTime;
        [SerializeField] private HarvestController harvestController;
        [SerializeField] private Plant _plantPrefab;
        [SerializeField] private Product _productPrefab;

        public ProductReference ProductReference => _productReference;
        public float GrowTime => _growTime;

        public HarvestController HarvestController => harvestController;
        
        public Plant PlantPrefab => _plantPrefab;
        public Product ProductPrefab => _productPrefab;
    }
}