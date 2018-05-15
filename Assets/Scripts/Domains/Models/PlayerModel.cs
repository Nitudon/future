using AGS.Domains;
using UnityEngine;
using UniRx;

/// <summary>
/// プレイヤーの同期モデル
/// </summary>
public class PlayerModel : SyncObjectModel<SyncPlayerData>
{
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
    /// プレイヤーの生成
    /// 同期すべきプレイヤーデータに基づいて生成
    /// </summary>
    /// <param name="user">ベースとなるプレイヤーデータ</param>
    /// <param name="transform">プレイヤーオブジェクトの親ルート</param>
    /// <param name="isMine">自身の操作オブジェクトであるかどうかのフラグ</param>
    /// <returns>プレイヤーオブジェクト</returns>
    public static PlayerModel CreateFromPlayerData(SyncPlayerData user, Transform transform, bool isMine = false)
    {
        // ベースプレハブからプレイヤーオブジェクトを読みだして指定ルートに生成
        var primitive = Resources.Load<PlayerModel>(PRIMITIVE_PATH);
        var player = Instantiate<PlayerModel>(primitive ,transform);

        // プレイヤーデータを流し込む
        player._playerId = user.PlayerId;
        player._name = user.Name;
        player._playerHp = new FloatReactiveProperty(user.Hp);
        player._isMine = isMine;

        if(isMine)
        {
            _myPlayer = player;
        }

        return player;
    }

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
}
