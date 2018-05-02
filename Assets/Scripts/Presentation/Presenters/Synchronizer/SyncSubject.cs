using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AGS.Domains;
using AGS.Websocket;
using UniRx;

/// <summary>
/// サーバーとの同期データの送受信クラス
/// Subjectで管理し、各種同期クラスが購読することでサーバーからの同期情報を反映、こちらからの同期情報の送信は
/// </summary>
public class SyncSubject : MonoBehaviour {

    public enum SyncType
    {
        Room = 0,
        Object,
        Player,
    }

    [System.Serializable]
    private class SyncMessage
    {
        public SyncType SyncType;
        public string Message;
    }

    /// <summary>
    /// 
    /// </summary>
    public Subject<SyncRoomData> RoomSubject = new Subject<SyncRoomData>();

    public Subject<SyncObjectData> ObjectSubject = new Subject<SyncObjectData>();

    public Subject<SyncPlayerData> PlayerSubject = new Subject<SyncPlayerData>();

    private Client _websocketClient;
    private SyncMessage _syncMessage;

    public void Initialize()
    {
        _websocketClient = new Client();
        _websocketClient.Connect();

        _syncMessage = new SyncMessage();
    }

    public void SendSyncData(SyncType type, string json)
    {
        _syncMessage.SyncType = type;
        _syncMessage.Message = json;
        _websocketClient.SendMessage(JsonUtility.ToJson(_syncMessage));
    }

    private void ReceiveSyncData()
    {

    }
}
