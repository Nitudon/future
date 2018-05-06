using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using AGS.Domains;
using AGS.WebRequest;
using UnityEngine;
using Zenject;

public class GameRulePresenter : MonoBehaviour {

    [Inject]
    private RoomModel _roomModel;

    public async Task SetupGame(string roomId)
    {
        var room = await RoomWebRequest.FetchRoomDataAsync(roomId);
        _roomModel.Initialize(room);
    }
}
