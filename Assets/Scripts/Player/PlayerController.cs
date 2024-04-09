using System;
using System.Linq;
using Gardening;
using Inventory;
using UnityEngine;
using Utils;
using Zenject;

public class PlayerController : MonoBehaviour
{
    private SelectController _selectController;
    private SeedContainer _seedContainer;
    private GameSettingInstaller.Settings _settings;
    private InventoryStorage _storage;

    [Inject]
    public void Construct(SelectController selectController, GameSettingInstaller.Settings settings, InventoryStorage storage)
    {
        _selectController = selectController;
        _settings = settings;
        _seedContainer = _settings.SeedContainer;
        _storage = storage;
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.CompareTag(Constants.GardenBedTag))
        {
            var selectedItem = _selectController.SelectedItem;
            if(selectedItem == null)
                return;
            
            var enoughAmount = _storage.GetItemCount(selectedItem.Data) == 0;
            if(enoughAmount)
                return;

            var gardenBed = other.gameObject.GetComponent<GardenBed>();
            if (gardenBed != null)
            {
                var targetSeedData =_seedContainer.Maps.FirstOrDefault(x => x.ItemData.Equals(selectedItem.Data));
                if (targetSeedData != null)
                {
                    gardenBed.TryPlantSeed(targetSeedData.plantData, delegate(bool result)
                    {
                        if (result)
                        {
                            _storage.RemoveItem(targetSeedData.ItemData, 1);
                        }
                    });
                }
            }
        }
    }
}
