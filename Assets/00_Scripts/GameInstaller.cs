using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller<GameInstaller>
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<ResourceModelUpdatedSignal>();
        Container.Bind<ResourceView>().AsSingle();
    }
}