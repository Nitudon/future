using System.Collections;
using AGS.Domains;
using UniRx;
using UnityEngine;

public class SyncObjectModel : MonoBehaviour {

    private ReactiveProperty<Vector3> _syncPosition;
    public IReadOnlyReactiveProperty<Vector3> PlayerPosition => _syncPosition;

    private Client _owner;

    private bool _isMine = true;
    public bool IsMine => _isMine;

    public static SyncObjectModel CreateSyncObject(){
        var model = Instantiate<SyncObjectModel>(new SyncObjectModel());

        model._isMine = false;

        return model;
    }

    public void SyncPosition()
    {
        MainThreadDispatcher.StartUpdateMicroCoroutine(UpdateCoroutine());
    }

    private IEnumerator UpdateCoroutine()
    {
        while (IsMine)
        {
            _syncPosition.Value = transform.position;
            yield return null;
        }
    }
}
