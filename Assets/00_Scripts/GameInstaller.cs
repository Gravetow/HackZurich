using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller<GameInstaller>
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<ResourceModelUpdatedSignal>();
        Container.DeclareSignal<TileClickedSignal>();
        Container.DeclareSignal<LeaveConstructionSignal>();

        Container.BindInterfacesTo<ResourceView>().AsSingle();
        Container.BindInterfacesTo<TileClick>().AsSingle();
        Container.BindInterfacesTo<CameraMovement>().AsSingle();
        Container.BindInterfacesTo<OverlayView>().AsSingle();
    }
}