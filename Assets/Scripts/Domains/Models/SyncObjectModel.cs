using System.Collections;
using UniRx;
using UnityEngine;

public class SyncObjectModel : MonoBehaviour {

    private ReactiveProperty<Vector3> _syncPosition;
    public IReadOnlyReactiveProperty<Vector3> PlayerPosition => _playerPosition;

    public void SyncPosition()
    {
        MainThreadDispatcher.StartUpdateMicroCoroutine(UpdateCoroutine());
    }

    private IEnumerator UpdateCoroutine()
    {
        while (true)
        {
            _playerPosition.Value = transform.position;
            yield return null;
        }
    }
}
