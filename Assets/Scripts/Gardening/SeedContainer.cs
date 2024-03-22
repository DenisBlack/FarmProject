using System;
using System.Collections;
using System.Collections.Generic;
using Gardening;
using Inventory;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SeedContainer", menuName = "SeedContainer")]
public class SeedContainer : ScriptableObject
{
    [SerializeField] public List<ItemAssetMap> Maps;
    
    
    [Serializable]
    public class ItemAssetMap
    {
        public ItemData ItemData;
        [FormerlySerializedAs("SeedData")] public PlantData plantData;
    }
}
