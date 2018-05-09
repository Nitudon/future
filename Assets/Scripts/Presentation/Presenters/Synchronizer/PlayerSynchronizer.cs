using System.Linq;
using AGS.Domains;
using UnityEngine;
using UniRx;
using Zenject;

public class PlayerSynchronizer : MonoBehaviour {

    [SerializeField]
    private SyncSubject _syncSubject;

    [SerializeField]
    private RoomModel _roomModel;

    private PlayerModel _myPlayer;

    public void Initialize()
    {
        _myPlayer = _roomModel.Players.FirstOrDefault(player => player.IsMine);
    }

    public void SendData(SyncPlayerData data)
    {
        _syncSubject.SendSyncData(SyncSubject.SyncType.Player, JsonUtility.ToJson(data));
    }

    public void ReceiveData(string message)
    {
        var data = JsonUtility.FromJson<SyncPlayerData>(message);

        if(data.Id >_roomModel.Players.Length - 1 || data.Id < 0 || _roomModel.Players == null)
        {
            Debug.LogError("Invalid Sync Data for Player");
            return;
        }

        var target = _roomModel.Players[data.Id];
        target.AffectSyncPlayerData(data);

    }
}
