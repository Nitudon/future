using System.Collections;
using System.Collections.Generic;
using AGS.Domains;
using UnityEngine;
using UniRx;
using UdonLib.Commons;
using UdonObservable.Commons;

public class RoomModel : UdonBehaviour {

    public RoomData RoomSetting;

    public UserData MasterUser => RoomSetting.Users[0];

    [SerializeField]
    private ReactiveProperty<int> _gameTimer;
    public IReadOnlyReactiveProperty<int> GameTimer => _gameTimer;

	public void Initialize(RoomData data){
        RoomSetting = data;
    }

    public void SetGameTimer(){
        _gameTimer = ReactiveTimer.ReactiveTimerForSeconds((int)RoomSetting.TotalGameTime);
    }
}
