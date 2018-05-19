using Zenject;
using UnityEngine;
using AGS.Domains;

public class RoomInstaller : MonoInstaller {

    [SerializeField]
    private PlayerModel _playerPrefab;

    [SerializeField]
    private Transform _playerParent;

    public override void InstallBindings()
    {
        Container.Bind<RoomModel>().AsSingle();
        Container.Bind<GameRulePresenter>().AsSingle();
        Container.Bind<RoomSynchronizer>().AsSingle();
        Container.Bind<PlayerSynchronizer>().AsSingle();
        Container.Bind<ObjectSynchronizer>().AsSingle();

        //Container.BindIFactory<SyncPlayerData, bool, PlayerModel, PlayerModel.PlayerFactory>().FromComponentInNewPrefab(_playerPrefab).UnderTransform(_playerParent);
    }
}
