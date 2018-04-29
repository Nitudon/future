using System.Linq;
using AGS.Domains;
using UnityEngine;
using UniRx;
using UdonLib.Commons;
using UdonObservable.Commons;

public class RoomModel : UdonBehaviour{

    [SerializeField]
    private ReactiveProperty<int> _gameTimer;
    public IReadOnlyReactiveProperty<int> GameTimer => _gameTimer;

    public RoomData RoomSetting;
    public UserData MasterUser => RoomSetting.Users[0];

    private PlayerModel[] _players;

	public void Initialize(RoomData data){
        RoomSetting = data;

        _players = RoomSetting.Users.Select()
    }

    public void SetGameTimer(){
        _gameTimer = ReactiveTimer.ReactiveTimerForSeconds((int)RoomSetting.TotalGameTime);
    }
}
