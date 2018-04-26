using System.Collections;
using UdonLib.Commons;
using UnityEngine;
using UniRx;

/// <summary>
/// プレイヤーのモデル
/// </summary>
public class PlayerModel : SyncObjectModel {

    private FloatReactiveProperty _playerHp;
    public IReadOnlyReactiveProperty<float> PlayerHp => _playerHp;

}
