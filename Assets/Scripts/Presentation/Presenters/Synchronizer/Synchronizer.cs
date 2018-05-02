using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AGS.Websocket;

public class Synchronizer : MonoBehaviour {

    private Client _websocketClient;

    public void Initialize()
    {
        _websocketClient = new Client();
        _websocketClient.Connect();
    }
}
