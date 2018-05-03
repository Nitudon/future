using AGS.Domains;
using UnityEngine;
using UniRx;
using Zenject;

public class ObjectSynchronizer : MonoBehaviour
{

    [Inject]
    private SyncSubject _syncSubject;

    [Inject]
    private RoomModel _roomModel;

    public void SendData(SyncObjectData data)
    {
        _syncSubject.SendSyncData(SyncSubject.SyncType.Object, JsonUtility.ToJson(data));
    }

    public void ReceiveData(string message)
    {
        var data = JsonUtility.FromJson<SyncObjectData>(message);

        if (data.Id > _roomModel.Players.Length - 1 || data.Id < 0 || _roomModel.Players == null)
        {
            Debug.LogError("Invalid Sync Data for Player");
            return;
        }

        var target = _roomModel.Players[data.Id];
        //target.AffectSyncPlayerData(data);

    }
}
