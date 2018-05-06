using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonLib.Commons;

public class GamePlayerView : UdonBehaviour {

    [SerializeField]
    private MeshRenderer _playerRenderer;

    public void ActivatePlayerView()
    {
        _playerRenderer.enabled = true;
    }

    public void SyncPosition(Vector3 position)
    {
        transform.position = position;
    }

}
