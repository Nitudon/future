using System.Collections;
using System.Collections.Generic;
using AGS.Domains;
using UnityEngine;
using UniRx;
using UdonObservable.Commons;

public class RoomModel : Component {

    public RoomData RoomSetting;

    [SerializeField]
    private ReactiveProperty<int> _gameTimer;
    public IReadOnlyReactiveProperty<int> GameTimer => _gameTimer;

    public RoomModel(RoomData data){
        RoomSetting = data;
    }

	public void Initialize(){
        _gameTimer = ReactiveTimer.ReactiveTimerForSeconds((int)RoomSetting.TotalGameTime);
    }

}
