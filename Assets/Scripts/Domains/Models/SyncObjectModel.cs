using System.Collections;
using AGS.Domains;
using UniRx;
using UnityEngine;
using UdonLib.Commons;
using System;

public class SyncObjectModel<T> : UdonBehaviour where T : SyncObjectData{

    protected ReactiveProperty<Vector3> _syncPosition;
    public IReadOnlyReactiveProperty<Vector3> SyncPosition => _syncPosition;

    protected string _id;
    public string Id => _id;

    protected UserData _owner;
    protected SyncObjectData _cachedSyncObjectData;

    public int OwnerId => _owner.UserId;

    [SerializeField]
    protected bool _isMine = true;
    public bool IsMine => _isMine;

    protected override void OnEnable()
    {
        if(_isMine)
        {
            StartSyncPosition();
        }
    }

    public SyncObjectData GetSyncBaseData()
    {
        Vector3 syncPosition = _syncPosition.Value;
        _cachedSyncObjectData.Id = _id;
        _cachedSyncObjectData.PositionX = syncPosition.x;
        _cachedSyncObjectData.PositionY = syncPosition.y;
        _cachedSyncObjectData.PositionZ = syncPosition.z;
        _cachedSyncObjectData.IsDestroyed = IsDestroyed;

        return _cachedSyncObjectData;
    }

    public void SetObjectData(string id, UserData owner)
    {
        _id = id;
        _owner = owner;
    }

    public virtual T GetSyncModelData() { return GetSyncBaseData() as T; }

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
        _isMine = true;
        MainThreadDispatcher.StartUpdateMicroCoroutine(UpdateCoroutine());
        _cachedSyncObjectData = new SyncObjectData();
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
