using System.Threading.Tasks;
using WebSocketSharp;
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

    [SerializeField]
    private RoomSynchronizer _roomSynchronizer;

    [SerializeField]
    private ObjectSynchronizer _objectSynchronizer;

    [SerializeField]
    private PlayerSynchronizer _playerSynchronizer;

    private Client _websocketClient;
    private SyncMessage _syncMessage;

    public async Task InitializeAsync()
    {
        _websocketClient = new Client();
        await _websocketClient.ConnectAsync();
        _websocketClient.OnReceiveMessageListener += ReceiveSyncData;

        _syncMessage = new SyncMessage();
    }

    public void SendSyncData(SyncType type, string json)
    {
        _syncMessage.SyncType = type;
        _syncMessage.Message = json;
        _websocketClient.SendMessage(JsonUtility.ToJson(_syncMessage));
    }

    private void ReceiveSyncData(object sender, MessageEventArgs e)
    {
        var data = JsonUtility.FromJson<SyncMessage>(e.Data);
        switch(data.SyncType)
        {
            case SyncType.Room:
                return;

            case SyncType.Object:
                return;

            case SyncType.Player:
                _playerSynchronizer.ReceiveData(data.Message);
                return;
        }
    }
}
