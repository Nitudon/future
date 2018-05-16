using System.Collections;
using AGS.Domains;
using UniRx;
using UnityEngine;
using UdonLib.Commons;
using System;

/// <summary>
/// 同期するオブジェクトモデル、ゲーム内で同期するオブジェクトのベース
/// </summary>
/// <typeparam name="T">クライアントサーバー間で送受信する同期データ構造</typeparam>
public class SyncObjectModel<T> : UdonBehaviour where T : SyncObjectData{

    /// <summary>
    /// 同期する座標
    /// </summary>
    protected ReactiveProperty<Vector3> _syncPosition = new ReactiveProperty<Vector3>(Vector3.zero);
    public IReadOnlyReactiveProperty<Vector3> SyncPosition => _syncPosition;

    /// <summary>
    /// オブジェクトの識別UUID
    /// </summary>
    protected string _id = Guid.NewGuid().ToString();
    public string Id => _id;

    /// <summary>
    /// オブジェクトのオーナー
    /// </summary>
    protected SyncPlayerData _owner;
    public int OwnerId => _owner.PlayerId;

    /// <summary>
    /// オブジェクトキャッシュ
    /// </summary>
    protected SyncObjectData _cachedSyncObjectData = new SyncObjectData();

    [SerializeField]
    protected bool _isMine = true;
    public bool IsMine => _isMine;

    /// <summary>
    /// アクティブイベント
    /// </summary>
    protected override void OnEnable()
    {
        // 自身が操作するオブジェクトに関してはデータを同期
        if(_isMine)
        {
            StartSyncPosition();
        }
    }

    /// <summary>
    /// 同期データの取得
    /// </summary>
    /// <returns>オブジェクトの同期データ</returns>
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

    /// <summary>
    /// オブジェクトの同期識別データの設定
    /// </summary>
    /// <param name="id">識別ID</param>
    /// <param name="owner">オーナー</param>
    public void SetObjectData(string id, SyncPlayerData owner)
    {
        _id = id;
        _owner = owner;
    }

    /// <summary>
    /// 同期データの反映
    /// </summary>
    /// <param name="data">同期データ</param>
    public void AffectSyncObjectData(SyncObjectData data)
    {
        // 破壊されていない限り位置同期
        if(data.IsDestroyed)
        {
            Destroy();
            return;
        }
        SetLocalPosition(data.PositionX, data.PositionY, data.PositionZ);
    }

    /// <summary>
    /// 位置同期の開始
    /// </summary>
    public void StartSyncPosition()
    {
        // 自身の捜査対象であることを定義して同期座標を更新、送受信用データオブジェクトのキャッシュを設定
        _isMine = true;
        MainThreadDispatcher.StartUpdateMicroCoroutine(UpdateCoroutine());
        _cachedSyncObjectData = new SyncObjectData();
    }

    /// <summary>
    /// 同期用のモデルデータ変更検知
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator UpdateCoroutine()
    {
        while (IsMine)
        {
            _syncPosition.Value = transform.position;
            yield return null;
        }
    }
}
