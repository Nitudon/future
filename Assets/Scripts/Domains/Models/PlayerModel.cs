﻿using AGS.Domains;
using UnityEngine;
using UniRx;
using Zenject;
using System.Collections;

/// <summary>
/// プレイヤーの同期モデル
/// </summary>
public class PlayerModel : SyncObjectModel<SyncPlayerData>
{
    [Inject]
    private Transform _playerCamera;

    [Inject]
    private RoomModel _roomModel;

    private Transform _playerTransform => _cachedTransform ?? (_cachedTransform = _playerCamera.GetComponent<Transform>());
    private Transform _cachedTransform;

    /// <summary>
    /// プレイヤープレハブのパス
    /// </summary>
    private static readonly string PRIMITIVE_PATH = "Prefabs/GamePlayer";

    /// <summary>
    /// プレイヤーの識別ID、プレイヤーリストのインデックス
    /// </summary>
    private int _playerId;
    public int PlayerId => _playerId;

    /// <summary>
    /// プレイヤーの表示名
    /// </summary>
    private string _name;
    public string Name => _name;

    /// <summary>
    /// プレイヤーのHP
    /// </summary>
    private FloatReactiveProperty _playerHp = new FloatReactiveProperty(100);
    public IReadOnlyReactiveProperty<float> PlayerHp => _playerHp;

    /// <summary>
    /// プレイヤーのアクティブイベント
    /// </summary>
    private BoolReactiveProperty _onActivated = new BoolReactiveProperty(false);
    public IReadOnlyReactiveProperty<bool> OnActivated => _onActivated;

    /// <summary>
    /// 自身の操作するプレイヤー
    /// </summary>
    private static PlayerModel _myPlayer;
    public static PlayerModel MyPlayer => _myPlayer;

    /// <summary>
    /// bulletの生成クラス
    /// </summary>
    private SyncBulletCreator _bulletCreator = new SyncBulletCreator();

    /// <summary>
    /// 同期プレイヤーデータの取得
    /// </summary>
    /// <returns>プレイヤーデータ</returns>
    public SyncPlayerData GetSyncModelData()
    {
        var data = new SyncPlayerData();
        data.Id = _id;
        data.PlayerId = _playerId;
        data.Name = _name;
        data.PositionX = _syncPosition.Value.x;
        data.PositionY = _syncPosition.Value.y;
        data.PositionZ = _syncPosition.Value.z;
        data.IsDestroyed = IsDestroyed;
        data.Hp = _playerHp.Value;

        return data;
    }

    /// <summary>
    /// アクティブの切り替え、可視化や同期処理の適応
    /// </summary>
    /// <param name="active">アクティブフラグ</param>
    public void SwitchActive(bool active)
    {
        _onActivated.Value = active;
    }

    /// <summary>
    /// 同期データの反映
    /// </summary>
    /// <param name="data"></param>
    public void AffectSyncPlayerData(SyncPlayerData data)
    {
        //AffectSyncObjectData(data);
        _playerHp.Value = data.Hp;
    }

    /// <summary>
    /// Bullet生成
    /// </summary>
    public void CreateBullet()
    {
        _bulletCreator.Create(GetSyncModelData(), _playerCamera.transform.TransformDirection(Vector3.forward));
    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <param name="damage">ダメージ量</param>
    public void DamageHp(float damage)
    {
    //正数処理
        if(damage > 0)
        {
            if(_playerHp.Value > damage)
            {
                _playerHp.Value = 0;
            }
            else
            {
                _playerHp.Value -= damage;
            }
        }
        else
        {
            Debug.LogError("Invalid damage for player");
        }
    }

    protected override IEnumerator UpdateCoroutine()
    {
        while (IsMine)
        {
            _syncPosition.Value = _playerCamera.transform.position;
            yield return null;
        }
    }

    #region [Factory]
    /// <summary>
    /// PlayerのFactory、DI処理
    /// </summary>
    public class PlayerFactory : IFactory<SyncPlayerData, bool, PlayerModel>
    {
        [Inject]
        private DiContainer _container;

        public PlayerModel Create(SyncPlayerData data, bool isMine)
        {
            var player = _container.Instantiate<PlayerModel>();

            // プレイヤーデータを流し込む
            player._playerId = data.PlayerId;
            player._name = data.Name;
            player._playerHp = new FloatReactiveProperty(data.Hp);
            player._isMine = isMine;

            if (isMine)
            {
                _myPlayer = player;
            }

            return player;
        }
    }
    #endregion
}
