using System.Threading.Tasks;
using AGS.WebRequest;
using UnityEngine;
using Zenject;

public class GameRulePresenter : MonoBehaviour {

    [Inject]
    private RoomModel _roomModel;

    [Inject]
    private SyncSubject _syncSubject;

    private void Start()
    {
        SetupGame("Room1").Start();
    }

    public async Task SetupGame(string roomId)
    {
        var room = await RoomWebRequest.FetchRoomDataAsync(roomId);
        _roomModel.Initialize(room);
        await _syncSubject.InitializeAsync();
    }
}
