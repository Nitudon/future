using AGS.Domains;
using UnityEngine;
using UniRx;

/// <summary>
/// プレイヤーのモデル
/// </summary>
public class PlayerModel : SyncObjectModel<SyncPlayerData>
{
    private static readonly string PRIMITIVE_PATH = "Prefabs/GamePlayer";

    private new int _id;
    public new int Id => _id;

    private FloatReactiveProperty _playerHp = new FloatReactiveProperty(100);
    public IReadOnlyReactiveProperty<float> PlayerHp => _playerHp;

    private BoolReactiveProperty _onActivated = new BoolReactiveProperty(false);
    public IReadOnlyReactiveProperty<bool> OnActivated => _onActivated;

    public static PlayerModel CreateFromPlayerData(SyncPlayerData user, Transform transform, bool mine = false)
    {
        var primitive = Resources.Load<PlayerModel>(PRIMITIVE_PATH);
        var player = Instantiate<PlayerModel>(primitive ,transform);

        player._id = user.Id;
        player._playerHp = new FloatReactiveProperty(user.Hp);
        player._isMine = mine;
        return player;
    }

    public override SyncPlayerData GetSyncModelData()
    {
        var data = new SyncPlayerData();
        data.Id = _id;
        data.PositionX = _syncPosition.Value.x;
        data.PositionY = _syncPosition.Value.y;
        data.PositionZ = _syncPosition.Value.z;
        data.IsDestroyed = IsDestroyed;
        data.Hp = _playerHp.Value;

        return data;
    }

    public void SwitchActive(bool active)
    {
        _onActivated.Value = active;
    }

    public void AffectSyncPlayerData(SyncPlayerData data)
    {
        AffectSyncObjectData(data);
        _playerHp.Value = data.Hp;
    }

    public void DamageHp(float damage)
    {
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
