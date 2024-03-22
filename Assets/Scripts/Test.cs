using System.Collections;
using System.Collections.Generic;
using Gardening;
using Gardening.UI;
using Inventory;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class Test : MonoBehaviour
{
    [SerializeField] private InventoryPopup _inventoryPopup;
    [SerializeField] private ItemData _itemData;
    [SerializeField] private ItemData _itemData2;
    
        //[SerializeField] private SeedItem _seedItem;
    [FormerlySerializedAs("_seedData")] [SerializeField] private PlantData plantData;
    [SerializeField] private GardenBed _gardenBed;


    [FormerlySerializedAs("_gardenTooltip")] [SerializeField] private GardenTimerTooltip gardenTimerTooltip;
    public float offset = 0.9f;
    private InventoryStorage _inventoryStorage;
    
    [Inject]
    public void Construct(InventoryStorage inventoryStorage)
    {
        _inventoryStorage = inventoryStorage;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var item = new Item(_itemData);
            
            _inventoryStorage.AddItem(item, 50);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            var item2 = new Item(_itemData2);
            
            _inventoryStorage.AddItem(item2, 50);
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _inventoryStorage.GetInventoryInfo();
            //_seedItem.Initialized(new Seed(plantData));
           // _gardenBed.TryPlantSeed(_seedItem);
            
           // _seed.Use(_gardenBed);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
          //  _inventoryPopup.gameObject.SetActive(!_inventoryPopup.gameObject.activeInHierarchy);
        }
    }
}
