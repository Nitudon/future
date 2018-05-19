using System.Threading.Tasks;
using WebSocketSharp;
using UnityEngine;
using AGS.Domains;
using AGS.Websocket;
using UniRx;

/// <summary>
/// サーバーとの同期データの送受信プロキシ
/// 各種同期オブジェクトとサーバーとのデータの送受信の窓口
/// </summary>
public class SyncSubject : MonoBehaviour {
    
    /// <summary>
    /// ルームの同期管理クラス
    /// </summary>
    [SerializeField]
    private RoomSynchronizer _roomSynchronizer;

    /// <summary>
    /// オブジェクトの同期管理クラス
    /// </summary>
    [SerializeField]
    private ObjectSynchronizer _objectSynchronizer;

    /// <summary>
    /// プレイヤーの同期管理クラス
    /// </summary>
    [SerializeField]
    private PlayerSynchronizer _playerSynchronizer;

    /// <summary>
    /// Websocketクライアント
    /// </summary>
    private Client _websocketClient;

    /// <summary>
    /// 送信するデータのベースキャッシュ
    /// </summary>
    private SyncMessage _syncMessage = new SyncMessage();

    /// <summary>
    /// 初期化
    /// </summary>
    /// <returns></returns>
    public async Task InitializeAsync()
    {
        // Websocket疎通と各種ハンドリング、同期クラスの初期化
        _websocketClient = new Client();
        await _websocketClient.ConnectAsync();
        _websocketClient.OnReceiveMessageListener += ReceiveSyncData;
        _playerSynchronizer.Initialize();
        _syncMessage = new SyncMessage();
    }

    /// <summary>
    /// 同期データの送信
    /// </summary>
    /// <param name="type">同期データの種類</param>
    /// <param name="json">同期データのjson文字列</param>
    public void SendSyncData(SyncType type, string json)
    {
        _syncMessage.SyncType = type;
        _syncMessage.Message = json;
        _websocketClient.SendMessage(JsonUtility.ToJson(_syncMessage));
    }

    /// <summary>
    /// 同期データの受信
    /// </summary>
    /// <param name="sender">送信者</param>
    /// <param name="e">受信したメッセージ</param>
    private void ReceiveSyncData(object sender, MessageEventArgs e)
    {
        // パースして各種データに合わせて同期処理を依頼
        var data = JsonUtility.FromJson<SyncMessage>(e.Data);
        switch(data.SyncType)
        {
            case SyncType.Room:
                _roomSynchronizer.ReceiveData(data.Message);
                return;

            case SyncType.Object:
                _objectSynchronizer.ReceiveData(data.Message);
                return;

            case SyncType.Player:
                _playerSynchronizer.ReceiveData(data.Message);
                return;
        }
    }
}
