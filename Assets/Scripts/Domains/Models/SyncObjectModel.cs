using System.Collections;
using AGS.Domains;
using UniRx;
using UnityEngine;

public class SyncObjectModel : MonoBehaviour {

    private ReactiveProperty<Vector3> _syncPosition;
    public IReadOnlyReactiveProperty<Vector3> PlayerPosition => _syncPosition;

    private UserData _owner;

    public int OwnerId => _owner.UserId;

    [SerializeField]
    private bool _isMine = true;
    public bool IsMine => _isMine;

    public static T CreateSyncObject<T>(UserData owner) where T : SyncObjectModel, new(){
        var model = Instantiate<T>(new T());

        model._owner = owner;
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
