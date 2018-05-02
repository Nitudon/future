using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerSynchronizer : MonoBehaviour {

    [SerializeField]
    private GamePlayerView _gamePlayerView;

    [SerializeField]
    private PlayerModel _gamePlayerModel;

    public void Initialize()
    {
        _gamePlayerModel.PlayerPosition
            .Subscribe(_gamePlayerView.SyncPosition);
    }

}
