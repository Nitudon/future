using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletModel : MonoBehaviour {

    private float _speed;
    private Vector3 _bulletDirection;

    public void Initialize(float speed, Vector3 dir)
    {
        _speed = speed;
        _bulletDirection = dir.normalized;
    }

    public void FixedUpdate()
    {
        transform.position += _bulletDirection * _speed;
    }
}
