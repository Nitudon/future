using AGS.Domains;
using UnityEngine;
using Zenject;

/// <summary>
/// オブジェクトの同期処理を管理するクラス
/// </summary>
public class ObjectSynchronizer : MonoBehaviour
{
    
    /// <summary>
    /// サーバーとの同期データの送受信プロキシ
    /// </summary>
    [Inject]
    private SyncSubject _syncSubject;

    /// <summary>
    /// 同期するルームモデル
    /// </summary>
    [Inject]
    private RoomModel _roomModel;

    /// <summary>
    /// 同期するオブジェクトデータの送信
    /// </summary>
    /// <param name="data"></param>
    public void SendData(SyncObjectData data)
    {
        _syncSubject.SendSyncData(SyncType.Object, JsonUtility.ToJson(data));
    }

    /// <summary>
    /// 同期するオブジェクトデータの受信
    /// </summary>
    /// <param name="message"></param>
    public void ReceiveData(string message)
    {
        var data = JsonUtility.FromJson<SyncObjectData>(message);

        /*if (data.Id > _roomModel.Players.Length - 1 || data.Id < 0 || _roomModel.Players == null)
        {
            Debug.LogError("Invalid Sync Data for Player");
            return;
        }

        var target = _roomModel.Players[data.Id];
        target.AffectSyncPlayerData(data);
        */

    }
}
