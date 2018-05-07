using Zenject;

public class RoomInstaller : MonoInstaller {

    public override void InstallBindings()
    {
        Container.Bind<RoomModel>().AsSingle();
        Container.Bind<SyncSubject>().AsSingle();
        Container.Bind<TrackingHandler>().AsSingle();
    }
}
