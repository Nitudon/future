using UnityEngine;

public class RoomUserModel : MonoBehaviour {

    private string _roomId;
    public string RoomId => _roomId;

    private string _roomName;
    public string RoomName => _roomName;

    private RoomAccessController _roomAccessController;
    public RoomAccessController RoomAccessController => _roomAccessController;

}
