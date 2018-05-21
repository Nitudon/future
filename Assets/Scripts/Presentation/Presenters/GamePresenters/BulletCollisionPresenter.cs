using System;
using System.Collections;
using System.Collections.Generic;
using UdonObservable.ColiderRx;
using AGS.Domains;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Zenject;

public class BulletCollisionPresenter : MonoBehaviour, IDisposable {

    [Inject]
    private RoomModel _roomModel;

    private float _damage = 10;

    private CompositeDisposable _disposable = new CompositeDisposable();

    public void Initialize()
    {
        _roomModel.SyncObjectPool.SyncObjects
            .ObserveAdd()
            .Subscribe(e => AddCollisionObserver(e.Value))
            .AddTo(_disposable);
    }

    public void Dispose()
    {
        _disposable.Dispose();
    }

    private void AddCollisionObserver(SyncObjectModel<SyncObjectData> model)
    {
        model
            .OnTriggerEnterAsObservable()
            .First()
            .Subscribe(x => OnCollisionWithPlayer(x, model))
            .AddTo(model);
    }

    public void OnCollisionWithPlayer(Collider col, SyncObjectModel<SyncObjectData> model)
    {
        if (col.tag == "Player")
        {
            var player = col.gameObject.GetComponent<PlayerModel>();
            if(player.PlayerId != model.OwnerId)
            {
                player.DamageHp(_damage);
            }
        }

        _roomModel.SyncObjectPool.Destroy(model.Id);
    }
}
