using System.Collections;
using UdonLib.Commons;
using AGS.Domains;
using UnityEngine;
using UniRx;

/// <summary>
/// プレイヤーのモデル
/// </summary>
public class PlayerModel : SyncObjectModel {

    private FloatReactiveProperty _playerHp;
    public IReadOnlyReactiveProperty<float> PlayerHp => _playerHp;

    public static PlayerModel CreateFromPlayerData(UserData user){

        var player = CreateSyncObject<PlayerModel>(user);

        player._playerHp = new FloatReactiveProperty(user.PlayerParam.Hp);
        return player;
    }
}
