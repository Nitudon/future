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

    #region [Factory]
    /// <summary>
    /// BulletのFactory、DI処理
    /// </summary>
    public class BulletFactory : IFactory<SyncObjectData, Transform, bool, SyncBulletModel>
    {
        [Inject]
        private DiContainer _container;

        [Inject]
        private UnityEngine.Object _prefab;

        public SyncBulletModel Create(SyncObjectData data, Transform root, bool isMine = false)
        {
            var bullet = _container.InstantiatePrefabForComponent<SyncBulletModel>(_prefab);

            // プレイヤーデータを流し込む
            bullet._id = data.Id;
            bullet._isMine = isMine;

            return bullet;
        }

    }
    #endregion
}
