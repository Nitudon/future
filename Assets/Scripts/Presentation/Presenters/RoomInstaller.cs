using Zenject;
using UnityEngine;

public class RoomInstaller : MonoInstaller {

    [SerializeField]
    private GameObject _playerPrefab;

    public override void InstallBindings()
    {
        Container.Bind<RoomModel>().AsSingle();
        Container.Bind<PlayerModel.PlayerFactory>().AsSingle().WithArguments(_playerPrefab);
        Container.Bind<RoomSynchronizer>().AsSingle();
        Container.Bind<PlayerSynchronizer>().AsSingle();
        Container.Bind<ObjectSynchronizer>().AsSingle();
    }
}
