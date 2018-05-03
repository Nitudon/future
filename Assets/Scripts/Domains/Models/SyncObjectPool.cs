using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Toolkit;

public class SyncObjectPool : ObjectPool<SyncObjectModel> {

    public Dictionary<int, SyncObjectModel> SyncObjects;

    private Transform _parentTransform;
    private SyncObjectModel _primitiveObject;

    public SyncObjectPool(Transform transform)
    {
        SyncObjects = new Dictionary<int, SyncObjectModel>();
        _primitiveObject = new SyncObjectModel();
        _parentTransform = transform;
    }

    protected override SyncObjectModel CreateInstance()
    {
        var model = GameObject.Instantiate<SyncObjectModel>(_primitiveObject, _parentTransform);
        return model;
    }
}
