using System;
using System.Collections.Generic;
using AGS.Domains;
using UnityEngine;
using UniRx.Toolkit;

/// <summary>
/// 同期オブジェクトの管理クラス、生成、削除
/// </summary>
public class SyncObjectPool : ObjectPool<SyncObjectModel<SyncObjectData>> {

    /// <summary>
    /// 識別IDを主キーとしたオブジェクトマップ
    /// </summary>
    public Dictionary<string, SyncObjectModel<SyncObjectData>> SyncObjects;

    /// <summary>
    /// 同期オブジェクトの親ルート
    /// </summary>
    private Transform _parentTransform;

    /// <summary>
    /// 同期オブジェクトのベース
    /// </summary>
    private SyncObjectModel<SyncObjectData> _primitiveObject;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="transform">同期オブジェクトの親ルート</param>
    public SyncObjectPool(Transform transform)
    {
        SyncObjects = new Dictionary<string, SyncObjectModel<SyncObjectData>>();
        _primitiveObject = new GameObject().AddComponent<SyncObjectModel<SyncObjectData>>();
        _parentTransform = transform;
    }

    /// <summary>
    /// 同期オブジェクトのインスタンス生成
    /// </summary>
    /// <returns>同期オブジェクトのインスタンス</returns>
    protected override SyncObjectModel<SyncObjectData> CreateInstance()
    {
        var model = GameObject.Instantiate<SyncObjectModel<SyncObjectData>>(_primitiveObject, _parentTransform);
        return model;
    }

    /// <summary>
    /// 同期オブジェクトの生成
    /// </summary>
    /// <param name="owner">オーナー</param>
    /// <returns>同期オブジェクト</returns>
    public SyncObjectModel<SyncObjectData> Create(SyncPlayerData owner)
    {
        // オブジェクトプールから貸出、同期データを流し込み作成
        var model = Rent();
        string id = Guid.NewGuid().ToString();

        model.SetObjectData(id, owner);
        SyncObjects.Add(id, model);

        return model;
    }

    /// <summary>
    /// 同期オブジェクトの削除
    /// </summary>
    /// <param name="id">同期オブジェクトの識別ID</param>
    public void Destroy(string id)
    {
        // オブジェクトプールに返却し、マップから削除
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
