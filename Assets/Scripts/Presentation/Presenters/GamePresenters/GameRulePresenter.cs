using System.Threading.Tasks;
using AGS.WebRequest;
using UnityEngine;
using Zenject;

public class GameRulePresenter : MonoBehaviour {

    [SerializeField]
    private RoomModel _roomModel;

    [SerializeField]
    private SyncSubject _syncSubject;

    private void Start()
    {
        SetupGame("Room1");
    }

    public async Task SetupGame(string roomId)
    {
        var room = await RoomWebRequest.JoinRoomAsync();
        _roomModel.Initialize(room);
        await _syncSubject.InitializeAsync();

    }
}
