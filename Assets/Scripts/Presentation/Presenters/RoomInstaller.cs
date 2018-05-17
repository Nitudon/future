using Zenject;
using UnityEngine;
using AGS.Domains;

public class RoomInstaller : MonoInstaller {

    public override void InstallBindings()
    {
        Container.Bind<GameRulePresenter>().AsSingle();
        Container.Bind<RoomSynchronizer>().AsSingle();
        Container.Bind<PlayerSynchronizer>().AsSingle();
        Container.Bind<ObjectSynchronizer>().AsSingle();
    }
}

public class InstacingInstaller : MonoInstaller<InstacingInstaller>
{
    [SerializeField]
    private GameObject _playerPrefab;

    public override void InstallBindings()
    {
        Container.Bind<PlayerModel>().FromFactory<SyncPlayerData, Transform, bool, PlayerModel.PlayerFactory>();
    }
}
