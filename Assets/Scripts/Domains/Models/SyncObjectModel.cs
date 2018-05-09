using System.Collections;
using AGS.Domains;
using UniRx;
using UnityEngine;
using UdonLib.Commons;

public class SyncObjectModel : UdonBehaviour {

    protected ReactiveProperty<Vector3> _syncPosition;
    public IReadOnlyReactiveProperty<Vector3> PlayerPosition => _syncPosition;

    protected string _id;
    public string Id => _id;

    protected UserData _owner;

    public int OwnerId => _owner.UserId;

    [SerializeField]
    protected bool _isMine = true;
    public bool IsMine => _isMine;

    public void SetObjectData(string id, UserData owner, bool isMine = false)
    {
        _id = id;
        _owner = owner;
        _isMine = isMine;
    }

    public void AffectSyncObjectData(SyncObjectData data)
    {
        if(data.IsDestroyed)
        {
            Destroy();
            return;
        }
        SetLocalPosition(data.PositionX, data.PositionY, data.PositionZ);
    }

    public void StartSyncPosition()
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
