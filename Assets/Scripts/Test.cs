using System.Collections;
using System.Collections.Generic;
using Effects;
using Gardening;
using Gardening.UI;
using Inventory;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class Test : MonoBehaviour
{
    [Header("Buffs")] 
    [SerializeField] private BuffData _buffData;
    [SerializeField] private BuffData _buffData2;

    private Buff buff;
    private Buff buff2;
    
    [Header("Items")]
    [SerializeField] private InventoryPopup _inventoryPopup;
    [SerializeField] private ItemData _itemData;
    [SerializeField] private ItemData _itemData2;
    
        //[SerializeField] private SeedItem _seedItem;
    [FormerlySerializedAs("_seedData")] [SerializeField] private PlantData plantData;
    [SerializeField] private GardenBed _gardenBed;


    [FormerlySerializedAs("_gardenTooltip")] [SerializeField] private GardenTimerTooltip gardenTimerTooltip;
    public float offset = 0.9f;
    private InventoryStorage _inventoryStorage;
    private BuffSystem _buffSystem;

    [Inject]
    public void Construct(InventoryStorage inventoryStorage, BuffSystem buffSystem)
    {
        _inventoryStorage = inventoryStorage;
        _buffSystem = buffSystem;
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
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            buff = new Buff();
            buff.Initialized(_buffData);
            _buffSystem.AddEffect(buff);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _buffSystem.RemoveBuff(buff);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            buff2 = new Buff();
            buff2.Initialized(_buffData2);
            _buffSystem.AddEffect(buff2);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            _buffSystem.RemoveBuff(buff2);
        }
    }
}
