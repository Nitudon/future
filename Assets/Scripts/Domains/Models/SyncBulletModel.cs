using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AGS.Domains;
using Zenject;

[RequireComponent(typeof(Collider))]
public class SyncBulletModel : SyncObjectModel<SyncObjectData> {

    [Inject]
    private RoomModel _roomModel;
    
    private float _damage;

	public void OnCollisionWithPlayer(PlayerModel player)
    {
        player.DamageHp(_damage);
        _roomModel.SyncObjectPool.Destroy(_id);
    }
}
