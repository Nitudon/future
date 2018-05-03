using System.Collections;
using UdonLib.Commons;
using AGS.Domains;
using UnityEngine;
using UniRx;

/// <summary>
/// プレイヤーのモデル
/// </summary>
public class PlayerModel : SyncObjectModel
{

    private int _id;
    public int Id => _id;

    private FloatReactiveProperty _playerHp;
    public IReadOnlyReactiveProperty<float> PlayerHp => _playerHp;

    public static PlayerModel CreateFromPlayerData(UserData user, Transform transform)
    {
        var player = Instantiate(new PlayerModel() ,transform);

        player._id = user.UserId;
        player._playerHp = new FloatReactiveProperty(user.PlayerParam.Hp);
        return player;
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
