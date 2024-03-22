using System.Collections;
using System.Collections.Generic;
using Gardening;
using Gardening.UI;
using UnityEngine;
using Zenject;

public class GrowingTimerFactory
{
    [Inject] private DiContainer _diContainer;
    private readonly GardenTimerTooltip _timerTooltip;
    private readonly Transform _canvasTooltip;
    
    private List<GardenTimerTooltip> _tooltipsList = new List<GardenTimerTooltip>();

    public GrowingTimerFactory(GardenTimerTooltip gardenTimerTooltip, Transform canvasTooltip)
    {
        _timerTooltip = gardenTimerTooltip;
        _canvasTooltip = canvasTooltip;
    }

    public void CreateGardenTimerTooltip(PlantData plantData, Transform targetTransform)
    {
        var tooltip = _diContainer.InstantiatePrefab(_timerTooltip, _canvasTooltip).GetComponent<GardenTimerTooltip>();
        tooltip.Initialized(plantData, targetTransform, Release);
        _tooltipsList.Add(tooltip);
    }

    public void Release(GardenTimerTooltip timerTooltip)
    {
        _tooltipsList.Remove(timerTooltip);
        Object.Destroy(timerTooltip.gameObject);
    }
}
