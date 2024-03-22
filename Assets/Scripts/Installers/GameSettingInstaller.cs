using System;
using Gardening;
using Gardening.UI;
using Orders;
using Shop;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Object = UnityEngine.Object;

[CreateAssetMenu(fileName = "GameSettingInstaller", menuName = "Installers/GameSettingInstaller")]
public class GameSettingInstaller : ScriptableObjectInstaller<GameSettingInstaller>
{
    public Settings GameSettings;
    public override void InstallBindings()
    {
        Container.BindInstance(GameSettings).IfNotBound();
    }
    
    [Serializable]
    public class Settings
    {
        public ProductTooltip ProductTooltip;
        public GardenTimerTooltip GardenTimerTooltip;
        public SeedContainer SeedContainer;
        public OrderContainer OrderContainer;
        public OrderTooltip OrderTooltip;
        public ShopProductContainer ShopProductContainer;
        public ShopElement ShopElement;
    }
}