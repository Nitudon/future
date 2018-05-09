using System;
using System.Collections.Generic;
using AGS.Domains;
using UnityEngine;
using UniRx.Toolkit;

public class SyncObjectPool : ObjectPool<SyncObjectModel<SyncObjectData>> {

    public Dictionary<string, SyncObjectModel<SyncObjectData>> SyncObjects;

    private Transform _parentTransform;
    private SyncObjectModel<SyncObjectData> _primitiveObject;

    public SyncObjectPool(Transform transform)
    {
        SyncObjects = new Dictionary<string, SyncObjectModel<SyncObjectData>>();
        _primitiveObject = new GameObject().AddComponent<SyncObjectModel<SyncObjectData>>();
        _parentTransform = transform;
    }

    protected override SyncObjectModel<SyncObjectData> CreateInstance()
    {
        var model = GameObject.Instantiate<SyncObjectModel<SyncObjectData>>(_primitiveObject, _parentTransform);
        return model;
    }

    public SyncObjectModel<SyncObjectData> Create(UserData owner)
    {
        var model = Rent();
        string id = Guid.NewGuid().ToString();

        model.SetObjectData(id, owner);
        SyncObjects.Add(id, model);

        return model;
    }

    public void Destroy(string id)
    {
        SyncObjectModel<SyncObjectData> model;
        if(SyncObjects.TryGetValue(id, out model))
        {
            Return(model);
            SyncObjects.Remove(id);
        }
        else
        {
            Debug.LogError("Invalid sync model id for deleting");
        }
    }
}
