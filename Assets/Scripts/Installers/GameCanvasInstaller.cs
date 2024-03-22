using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameCanvasInstaller : MonoInstaller
{
    [Inject] private GameSettingInstaller.Settings _settings;

    [SerializeField] private HotBar _hotBar;
    [SerializeField] private Transform _canvasTooltip;
    public override void InstallBindings()
    {
        Container.BindInstance(_hotBar);
    }
}
