using System.Linq;
using System.Collections.Generic;
using AGS.Domains;
using UnityEngine;
using UniRx;
using UdonLib.Commons;
using UdonObservable.Commons;
using Zenject;

public class RoomModel : UdonBehaviour{

    [Inject]
    private TrackingHandler _trackingHandler;

    [Inject]
    private SyncSubject _syncSubject;

    [SerializeField]
    private Transform _syncObjectRoot;

    [SerializeField]
    private Transform _syncPlayerRoot;

    [SerializeField]
    private IntReactiveProperty _gameTimer;
    public IReadOnlyReactiveProperty<int> GameTimer => _gameTimer;

    public SyncRoomData RoomSetting;
    public SyncPlayerData MasterUser => RoomSetting.Players[0];

    private PlayerModel[] _players;
    public PlayerModel[] Players => _players;

    private SyncObjectPool _syncObjectPool;
    public Dictionary<string, SyncObjectModel<SyncObjectData>> SyncObjects => _syncObjectPool.SyncObjects;

	public void Initialize(SyncRoomData data)
    {
        RoomSetting = data;

        _players = RoomSetting.Players.Select(player => PlayerModel.CreateFromPlayerData(player, _syncPlayerRoot)).ToArray();
        _players.LastOrDefault().StartSyncPosition();
        _syncObjectPool = new SyncObjectPool(_syncObjectRoot);

        //_trackingHandler.OnTrackingFoundStatusChanged
        //    .Where(status => status)
        //    .Subscribe(_ => ActivateRoom())
        //    .AddTo(this);
    }

    private void ActivateRoom()
    {
        _players.ForEach(player => player.SwitchActive(true));
    }

    public void SetGameTimer()
    {
        //_gameTimer = ReactiveTimer.ReactiveTimerForSeconds((int)RoomSetting.TotalGameTime) as IntReactiveProperty;
    }
}
