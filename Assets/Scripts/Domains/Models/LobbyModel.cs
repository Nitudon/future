using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using AGS.Domains;
using AGS.WebRequest;

public class LobbyModel : MonoBehaviour {

    private SyncRoomData[] _rooomDatas;

    private async void Start()
    {
        _rooomDatas = await RoomWebRequest.FetchSyncRoomDataAsync();
    }

    public void JoinRoom()
    {

    }

}
