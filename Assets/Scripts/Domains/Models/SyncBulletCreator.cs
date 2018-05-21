using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AGS.Domains;
using Zenject;

public class SyncBulletCreator {

    [Inject]
    private RoomModel _roomModel;
    
    private float _damage;

    public void Create(SyncPlayerData owner, Vector3 dir)
    {
        var obj =_roomModel.SyncObjectPool.Create(owner);
        var bullet = obj.GetComponent<BulletModel>();
        bullet.Initialize(10f, dir);
    }

}
