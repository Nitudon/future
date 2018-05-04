using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class RoomUserModel : MonoBehaviour {

    private string _roomId;
    public string RoomId => _roomId;

    private RoomAccessController _roomAccessController;
    public RoomAccessController roomAccessController => _roomAccessController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
