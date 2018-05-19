using Zenject;
using UnityEngine;
using AGS.Domains;

public class RoomInstaller : MonoInstaller {

    [SerializeField]
    private GameObject _playerPrefab;

    public override void InstallBindings()
    {
        Container.Bind<GameRulePresenter>().AsSingle();
        Container.Bind<RoomSynchronizer>().AsSingle();
        Container.Bind<PlayerSynchronizer>().AsSingle();
        Container.Bind<ObjectSynchronizer>().AsSingle();

        Container.BindFactory<SyncPlayerData, Transform, bool, PlayerModel, PlayerModel.PlayerFactory>().FromComponentInNewPrefab(_playerPrefab);
    }
}
