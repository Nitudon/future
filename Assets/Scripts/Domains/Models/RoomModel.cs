﻿using System.Linq;
using System.Collections.Generic;
using AGS.Domains;
using UnityEngine;
using UniRx;
using UdonLib.Commons;
using UdonObservable.Commons;
using Zenject;

/// <summary>
/// ルームの同期モデル
/// </summary>
public class RoomModel : UdonBehaviour{

    /// <summary>
    /// 同期データの送受信プロキシ
    /// </summary>
    [SerializeField]
    private SyncSubject _syncSubject;

    /// <summary>
    /// ARカメラのトラッキングイベントのハンドラー
    /// </summary>
    [SerializeField]
    private TrackingHandler _trackingHandler;

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
    /// オーナー判定
    /// </summary>
    private bool _isMaster;
    public bool IsMaster => _isMaster;

    /// <summary>
    /// 参加しているプレイヤーモデル
    /// </summary>
    private PlayerModel[] _players;
    public PlayerModel[] Players => _players;

    /// <summary>
    /// 試合までの参加プレイヤーの入室リスト
    /// </summary>
    private ReactiveCollection<PlayerModel> _roomPlayerJoinList = new ReactiveCollection<PlayerModel>();
    public IReactiveCollection<PlayerModel> RoomPlayerJoinList => _roomPlayerJoinList;

    /// <summary>
    /// 同期するオブジェクト群と生成管理システム
    /// </summary>
    private SyncObjectPool _syncObjectPool;
    public SyncObjectPool SyncObjectPool => _syncObjectPool;

    /// <summary>
    /// ゲーム中かどうかのフラグ
    /// </summary>
    private bool _isPlaying = false;
    public bool IsPlaying => _isPlaying;

    /// <summary>
    /// プレイヤーのDIファクトリー
    /// </summary>
    private PlayerModel.PlayerFactory _playerFactory;

    [Inject]
    public void Construct(PlayerModel.PlayerFactory factory)
    {
        _playerFactory = factory;
    }

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="data">同期するルームデータ</param>
	public void Initialize(SyncRoomData data)
    {
        RoomSetting = data;

        _roomPlayerJoinList = new ReactiveCollection<PlayerModel>(RoomSetting.Players.Select(player => _playerFactory.Create(player, player.PlayerId == RoomSetting.Players.Length - 1)));
        _syncObjectPool = new SyncObjectPool(_syncObjectRoot);

        // 自分しかいなければ自分がマスター
        _isMaster = _players.Length == 1;

        _trackingHandler.OnTrackingFoundStatusChanged
            .Where(status => status)
            .Subscribe(_ => ActivateRoom())
            .AddTo(this);
    }

    /// <summary>
    /// 同期ルームデータの取得
    /// </summary>
    /// <returns>ルームデータ</returns>
    public SyncRoomData GetSyncGameRoomData()
    {
        var data = new SyncRoomData();
        data.Time = _gameTimer.Value;
        return data;
    }

    /// <summary>
    /// 同期ルームデータの取得
    /// </summary>
    /// <returns>ルームデータ</returns>
    public SyncRoomData GetSyncJoinRoomData()
    {
        var data = new SyncRoomData();
        data.Players = _roomPlayerJoinList.Select(player => player.GetSyncModelData()).ToArray();
        return data;
    }

    public void AffectSyncRoomData(SyncRoomData data)
    {
        _gameTimer.Value = (int)data.Time;
    }

    private void JoinRoom(SyncPlayerData playerData)
    {
        _roomPlayerJoinList.Add(_playerFactory.Create(playerData, playerData.PlayerId == RoomSetting.Players.Length - 1));
    }

    private void LeaveRoom(int playerId)
    {
        _roomPlayerJoinList.RemoveAt(playerId);
        if(_roomPlayerJoinList.ElementAtOrDefault(0).IsMine)
        {
            _isMaster = true;
        }
    }

    /// <summary>
    /// ルームのアクティベート
    /// </summary>
    private void ActivateRoom()
    {
        _players = RoomSetting.Players.Select(player => _playerFactory.Create(player, player.PlayerId == RoomSetting.Players.Length - 1)).ToArray();

        var myPlayer = _players.FirstOrDefault(player => player.IsMine);
        myPlayer.StartSyncPosition();
        _players.ForEach(player =>
            {
                player.SwitchActive(true);
                player.GetComponent<PlayerPresenter>().Initialize();
            }
        );

        // ゲーム内タイマーの初期化と運用開始
        _gameTimer = ReactiveTimer.ReactiveTimerForSeconds((int)RoomSetting.Time) as IntReactiveProperty;
    }

}
