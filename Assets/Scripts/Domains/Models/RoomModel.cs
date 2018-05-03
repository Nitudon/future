using System.Linq;
using System.Collections.Generic;
using AGS.Domains;
using UnityEngine;
using UniRx;
using UdonLib.Commons;
using UdonObservable.Commons;

public class RoomModel : UdonBehaviour{

    [SerializeField]
    private Transform _syncObjectRoot;

    [SerializeField]
    private ReactiveProperty<int> _gameTimer;
    public IReadOnlyReactiveProperty<int> GameTimer => _gameTimer;

    public RoomData RoomSetting;
    public UserData MasterUser => RoomSetting.Users[0];

    private PlayerModel[] _players;
    public PlayerModel[] Players => _players;

    private SyncObjectPool _syncObjectPool;
    public Dictionary<string, SyncObjectModel> SyncObjects => _syncObjectPool.SyncObjects;

	public void Initialize(RoomData data){
        RoomSetting = data;

        _players = RoomSetting.Users.Select(PlayerModel.CreateFromPlayerData).ToArray();
        _syncObjectPool = new SyncObjectPool(_syncObjectRoot);
    }

    public void SetGameTimer(){
        _gameTimer = ReactiveTimer.ReactiveTimerForSeconds((int)RoomSetting.TotalGameTime);
    }
}
