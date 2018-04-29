using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GamePlayerCreator : MonoBehaviour {

    [Inject]
    private RoomModel _roomModel;

    public void CreatePlayer()
    {

    }
}
