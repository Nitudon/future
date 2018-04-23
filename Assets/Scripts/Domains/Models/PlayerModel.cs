using System.Collections;
using UdonLib.Commons;
using UnityEngine;
using UniRx;

/// <summary>
/// プレイヤーのモデル
/// </summary>
public class PlayerModel : UdonBehaviour {

    private FloatReactiveProperty _playerHp;
    public IReadOnlyReactiveProperty<float> PlayerHp => _playerHp;

    private ReactiveProperty<Vector3> _playerPosition;
    public IReadOnlyReactiveProperty<Vector3> PlayerPosition => _playerPosition;

    public void SyncPosition()
    {
        MainThreadDispatcher.StartUpdateMicroCoroutine(UpdateCoroutine());
    }

    private IEnumerator UpdateCoroutine()
    {
        while(true)
        {
            _playerPosition.Value = transform.position;
            yield return null;
        }
    }
}
