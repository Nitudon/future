using System.Linq;
using System.Collections.Generic;
using AGS.Domains;
using UnityEngine;
using UniRx;
using UdonLib.Commons;
using Zenject;

/// <summary>
/// ルームの同期モデル
/// </summary>
public class RoomModel : UdonBehaviour{

    /// <summary>
    /// ARカメラのトラッキングイベントのハンドラー
    /// </summary>
    [Inject]
    private TrackingHandler _trackingHandler;

    /// <summary>
    /// 同期データの送受信プロキシ
    /// </summary>
    [Inject]
    private SyncSubject _syncSubject;

    /// <summary>
    /// 同期オブジェクトの親ルート
    /// </summary>
    [SerializeField]
    private Transform _syncObjectRoot;

    /// <summary>
    /// 同期プレイヤーの親ルート
    /// </summary>
    [SerializeField]
    private Transform _syncPlayerRoot;

    /// <summary>
    /// ゲーム時間のタイマー
    /// </summary>
    [SerializeField]
    private IntReactiveProperty _gameTimer;
    public IReadOnlyReactiveProperty<int> GameTimer => _gameTimer;

    /// <summary>
    /// ルームの設定データ、初期化した同期データ
    /// </summary>
    public SyncRoomData RoomSetting;

    /// <summary>
    /// ルームを作成したオーナーのプレイヤーデータ
    /// </summary>
    public SyncPlayerData MasterUser => RoomSetting.Players[0];

    /// <summary>
    /// 参加しているプレイヤーモデル
    /// </summary>
    private PlayerModel[] _players;
    public PlayerModel[] Players => _players;

    /// <summary>
    /// 同期するオブジェクト群と生成管理システム
    /// </summary>
    private SyncObjectPool _syncObjectPool;
    public Dictionary<string, SyncObjectModel<SyncObjectData>> SyncObjects => _syncObjectPool.SyncObjects;

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="data">同期するルームデータ</param>
	public void Initialize(SyncRoomData data)
    {
        RoomSetting = data;

        _players = RoomSetting.Players.Select(player => PlayerModel.CreateFromPlayerData(player, _syncPlayerRoot)).ToArray();
        _players.LastOrDefault().StartSyncPosition();
        _syncObjectPool = new SyncObjectPool(_syncObjectRoot);

        //_trackingHandler.OnTrackingFoundStatusChanged
        //    .Where(status => status)
        //    .Subscribe(_ => ActivateRoom())
        //    .AddTo(this);
    }

    /// <summary>
    /// ルームのアクティベート
    /// </summary>
    private void ActivateRoom()
    {
        _players.ForEach(player => player.SwitchActive(true));
    }

    /// <summary>
    /// ゲーム内タイマーの初期化と運用開始
    /// </summary>
    public void SetGameTimer()
    {
        //_gameTimer = ReactiveTimer.ReactiveTimerForSeconds((int)RoomSetting.TotalGameTime) as IntReactiveProperty;
    }
}
