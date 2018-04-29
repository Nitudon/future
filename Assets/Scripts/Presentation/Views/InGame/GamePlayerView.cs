using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonLib.Commons;

public class GamePlayerView : UdonBehaviour {

    public void SyncPosition(Vector3 position)
    {
        transform.position = position;
    }

}
