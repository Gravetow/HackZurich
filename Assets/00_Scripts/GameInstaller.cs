using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller<GameInstaller>
{
    public HouseModel houseModel;
    public CarModel carModel;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<ResourceModelUpdatedSignal>();
        Container.DeclareSignal<TileClickedSignal>();
        Container.DeclareSignal<LeaveConstructionSignal>();
        Container.DeclareSignal<WorkerPercentageCalculatedSignal>();
        Container.DeclareSignal<NotificationSignal>();
        Container.DeclareSignal<AddMoneySignal>();
        Container.DeclareSignal<AddWorkerSignal>();
        Container.DeclareSignal<SubstractMoneySignal>();
        Container.DeclareSignal<SubstractWorkerSignal>();

        Container.Bind<HouseModel>().FromInstance(houseModel);
        Container.Bind<CarModel>().FromInstance(carModel);

        Container.BindInterfacesTo<ResourceView>().AsSingle();
        Container.BindInterfacesTo<TileClick>().AsSingle();
        Container.BindInterfacesTo<CameraMovement>().AsSingle();
        Container.BindInterfacesTo<OverlayView>().AsSingle();
        Container.BindInterfacesTo<SpawnCars>().AsSingle();
        Container.BindInterfacesTo<NotificationSignal>().AsSingle();
        Container.BindInterfacesTo<OpenBankingAPI>().AsSingle();
        Container.BindInterfacesTo<SignalEmitter>().AsSingle();


    }
}