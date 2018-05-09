using System.Linq;
using AGS.Domains;
using UnityEngine;
using UniRx;
using UdonLib.Commons;
using Zenject;

public class PlayerSynchronizer : UdonBehaviour {

    [SerializeField]
    private SyncSubject _syncSubject;

    [SerializeField]
    private RoomModel _roomModel;

    private PlayerModel _myPlayer;
    private CompositeDisposable _disposable = new CompositeDisposable();

    public void Initialize()
    {
        _myPlayer = _roomModel.Players.FirstOrDefault(player => player.IsMine);
        _myPlayer.SyncPosition.Subscribe(_ => SendData(_myPlayer.GetSyncModelData())).AddTo(_disposable);
        _myPlayer.PlayerHp.Subscribe(_ => SendData(_myPlayer.GetSyncModelData())).AddTo(_disposable);
    }

    private void SendData(SyncPlayerData data)
    {
        _syncSubject.SendSyncData(SyncSubject.SyncType.Player, JsonUtility.ToJson(data));
    }

    public void ReceiveData(string message)
    {
        var data = JsonUtility.FromJson<SyncPlayerData>(message);

        if(data.Id >_roomModel.Players.Length - 1 || data.Id < 0 || _roomModel.Players == null)
        {
            Debug.LogError("Invalid Sync Data for Player");
            return;
        }

        var target = _roomModel.Players[data.Id];
        target.AffectSyncPlayerData(data);
    }

    private void OnDestroy()
    {
        _disposable.Dispose();
    }
}
