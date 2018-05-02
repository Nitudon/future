using UnityEngine;
using UniRx;

public class PlayerPresenter : MonoBehaviour {

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
