using System.Collections;
using System.Collections.Generic;
using AGS.Domains;
using UnityEngine;
using UniRx;

public class RoomAccessController
{

    private ReactiveCollection<string> _roomPlayers;
    public IReadOnlyReactiveCollection<string> RoomPlayers => _roomPlayers;

    public void CreateRoom()
    {
        _roomPlayers = new ReactiveCollection<string>();
    }

    public int JoinRoom(string name)
    {
        if (_roomPlayers == null)
        {
            return -1;
        }

        _roomPlayers.Add(name);
        return _roomPlayers.Count;
    }

    public void LeftRoom(int id)
    {
        if (_roomPlayers == null || _roomPlayers.Count < id)
        {
            return;
        }


    }
}
