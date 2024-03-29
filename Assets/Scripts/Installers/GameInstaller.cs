using System.Collections;
using System.Collections.Generic;
using Buffs;
using Effects;
using Gardening;
using Gardening.Factory;
using Inventory;
using Orders;
using Popups;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Inject] private GameSettingInstaller.Settings _settings;

    [SerializeField] private HotBar _hotBar;
    [SerializeField] private Transform _canvasTooltip;
    [SerializeField] private RectTransform _popupsContainer;

    public override void InstallBindings()
    {
        Container.Bind<BuffSystem>().AsSingle().NonLazy();
        Container.Bind<BuffFactory>().AsSingle().NonLazy();
        Container.Bind<BuffElementFactory>().AsSingle().WithArguments(_settings.BuffElement).NonLazy();
        
        Container.BindInterfacesAndSelfTo<GardenSystem>().AsSingle().NonLazy();
        
        Container.BindInterfacesAndSelfTo<CoinSystem>().AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<OrderSystem>().AsSingle().NonLazy();
        Container.Bind<OrderTooltipFactory>().AsSingle().WithArguments(_settings.OrderTooltip, _canvasTooltip);
        Container.Bind<OrderController>().AsSingle().NonLazy();

        Container.Bind<PopupFactory>().AsSingle().WithArguments(_popupsContainer).NonLazy();
        Container.Bind<PopupSystem>().AsSingle().NonLazy();
        
        Container.Bind<PlantFactory>().AsSingle().NonLazy();
        Container.Bind<GrowingTimerFactory>().AsSingle().WithArguments(_settings.GardenTimerTooltip, _canvasTooltip).NonLazy();
        Container.Bind<ProductTooltipFactory>().AsSingle().WithArguments(_settings.ProductTooltip, _canvasTooltip).NonLazy();
        Container.Bind<ProductFactory>().AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<InventoryStorage>().AsSingle().NonLazy();

        Container.BindInstance(_hotBar);
        
        Container.Bind<SelectController>().AsSingle().NonLazy();
    }
}
