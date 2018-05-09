using AGS.Domains;
using UnityEngine;
using UniRx;

/// <summary>
/// プレイヤーのモデル
/// </summary>
public class PlayerModel : SyncObjectModel
{
    private static readonly string PRIMITIVE_PATH = "Prefabs/GamePlayer";

    private new int _id;
    public new int Id => _id;

    private FloatReactiveProperty _playerHp;
    public IReadOnlyReactiveProperty<float> PlayerHp => _playerHp;

    private BoolReactiveProperty _onActivated = new BoolReactiveProperty(false);
    public IReadOnlyReactiveProperty<bool> OnActivated => _onActivated;

    public static PlayerModel CreateFromPlayerData(PlayerData user, Transform transform, bool mine = false)
    {
        var primitive = Resources.Load<PlayerModel>(PRIMITIVE_PATH);
        var player = Instantiate<PlayerModel>(primitive ,transform);

        player._id = user.Id;
        player._playerHp = new FloatReactiveProperty(user.Hp);
        player._isMine = mine;
        return player;
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
