using System.Linq;
using AGS.Domains;
using UnityEngine;
using UniRx;
using UdonLib.Commons;
using Zenject;

/// <summary>
/// プレイヤーの同期処理を管理するクラス
/// </summary>
public class PlayerSynchronizer : UdonBehaviour {
    
    /// <summary>
    /// サーバーとの同期の送受信プロキシ
    /// </summary>
    [Inject]
    private SyncSubject _syncSubject;

    /// <summary>
    /// 同期するルームモデル
    /// </summary>
    [Inject]
    private RoomModel _roomModel;

    /// <summary>
    /// 自身が操作するプレイヤーモデル
    /// </summary>
    private PlayerModel _myPlayer;

    /// <summary>
    /// 同期処理のDisposable
    /// </summary>
    private CompositeDisposable _disposable = new CompositeDisposable();

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {
        //自身が操作するプレイヤーに関してモデルのデータを同期
        _myPlayer = _roomModel.Players.FirstOrDefault(player => player.IsMine);
        _myPlayer.SyncPosition.Subscribe(_ => SendData(_myPlayer.GetSyncModelData())).AddTo(_disposable);
        _myPlayer.PlayerHp.Subscribe(_ => SendData(_myPlayer.GetSyncModelData())).AddTo(_disposable);
    }

    /// <summary>
    /// 同期するデータの送信
    /// </summary>
    /// <param name="data"></param>
    private void SendData(SyncPlayerData data)
    {
        _syncSubject.SendSyncData(SyncType.Player, JsonUtility.ToJson(data));
    }

    /// <summary>
    /// 同期するデータの受信
    /// </summary>
    /// <param name="message">同期するデータの文字列</param>
    public void ReceiveData(string message)
    {
        //同期データにパースし、対象となるプレイヤーモデルに反映
        var data = JsonUtility.FromJson<SyncPlayerData>(message);
        if(data.PlayerId >_roomModel.Players.Length - 1 || data.PlayerId < 0 || _roomModel.Players == null)
        {
            Debug.LogError("Invalid Sync Data for Player");
            return;
        }

        var target = _roomModel.Players[data.PlayerId];
        target.AffectSyncPlayerData(data);
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
