using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using AGS.Domains;
using UnityEngine;
using Zenject;

public class GameRulePresenter : MonoBehaviour {

    [Inject]
    private RoomModel _roomModel;

    public async Task SetupGame(RoomData roomdata)
    {
        _roomModel.Initialize(roomdata);
    }
}
