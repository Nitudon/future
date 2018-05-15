using System.Linq;
using AGS.Domains;
using UnityEngine;
using UniRx;
using UdonLib.Commons;

/// <summary>
/// ルームの同期処理の管理を行うクラス
/// </summary>
public class RoomSynchronizer: MonoBehaviour {

    /// <summary>
    /// サーバーとの同期の送受信プロキシ
    /// </summary>
    [SerializeField]
    private SyncSubject _syncSubject;

    /// <summary>
    /// 同期するルームモデル
    /// </summary>
    [SerializeField]
    private RoomModel _roomModel;

    /// <summary>
    /// 同期処理のDisposable
    /// </summary>
    private CompositeDisposable _disposable = new CompositeDisposable();

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {
        //自身が操作するプレイヤーに関してルームのデータを同期
        if(_roomModel.IsMaster)
        {
            _roomModel.RoomPlayerJoinList.ObserveAdd().Subscribe(_ => _syncSubject.SendSyncData(SyncType.Room, JsonUtility.ToJson(_roomModel.GetSyncJoinRoomData()))).AddTo(_disposable);
            _roomModel.RoomPlayerJoinList.ObserveRemove().Subscribe(_ => _syncSubject.SendSyncData(SyncType.Room, JsonUtility.ToJson(_roomModel.GetSyncJoinRoomData()))).AddTo(_disposable);
            _roomModel.GameTimer.Subscribe(_ => _syncSubject.SendSyncData(SyncType.Room, JsonUtility.ToJson(_roomModel.GetSyncGameRoomData()))).AddTo(_disposable);
        }

    } 

    /// <summary>
    /// 同期するルームデータの送信
    /// </summary>
    /// <param name="data">ルームデータ</param>
    private void SendData(SyncRoomData data)
    {
        _syncSubject.SendSyncData(SyncType.Room, JsonUtility.ToJson(data));
    }

    /// <summary>
    /// 同期するデータの受信
    /// </summary>
    /// <param name="message">同期するデータの文字列</param>
    public void ReceiveData(string message)
    {
        //同期データにパースし、対象となるプレイヤーモデルに反映
        var data = JsonUtility.FromJson<SyncRoomData>(message);
        _roomModel.AffectSyncRoomData(data);
    }

    /// <summary>
    /// 削除時のイベント
    /// </summary>
    private void OnDestroy()
    {
        //同期処理を解放
        _disposable.Dispose();
    }

}
