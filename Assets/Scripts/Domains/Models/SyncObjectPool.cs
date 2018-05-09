using System;
using System.Collections.Generic;
using AGS.Domains;
using UnityEngine;
using UniRx.Toolkit;

public class SyncObjectPool : ObjectPool<SyncObjectModel> {

    public Dictionary<string, SyncObjectModel> SyncObjects;

    private Transform _parentTransform;
    private SyncObjectModel _primitiveObject;

    public SyncObjectPool(Transform transform)
    {
        SyncObjects = new Dictionary<string, SyncObjectModel>();
        _primitiveObject = new GameObject().AddComponent<SyncObjectModel>();
        _parentTransform = transform;
    }

    protected override SyncObjectModel CreateInstance()
    {
        var model = GameObject.Instantiate<SyncObjectModel>(_primitiveObject, _parentTransform);
        return model;
    }

    public SyncObjectModel Create(UserData owner)
    {
        var model = Rent();
        string id = Guid.NewGuid().ToString();

        model.SetObjectData(id, owner);
        SyncObjects.Add(id, model);

        return model;
    }

    public void Destroy(string id)
    {
        SyncObjectModel model;
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
