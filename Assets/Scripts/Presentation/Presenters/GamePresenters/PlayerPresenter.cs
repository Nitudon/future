using UnityEngine;
using UniRx;

public class PlayerPresenter : MonoBehaviour, System.IDisposable {

    [SerializeField]
    private GamePlayerView _gamePlayerView;

    [SerializeField]
    private PlayerModel _gamePlayerModel;

    [SerializeField]
    private GamePlayerUI _gamePlayerUI;

    private CompositeDisposable _disposable = new CompositeDisposable();

    public void Initialize()
    {
        _gamePlayerModel.PlayerPosition
            .Subscribe(_gamePlayerView.SyncPosition)
            .AddTo(_disposable);

        if (_gamePlayerModel.IsMine)
        {
            _gamePlayerModel.PlayerHp
                .Subscribe(_gamePlayerUI.UpdatePlayerUI)
                .AddTo(_disposable);
        }
    
        _gamePlayerModel.OnActivated
            .Where(active => active)
            .First()
            .Subscribe(_ =>
                {
                    _gamePlayerView.ActivatePlayerView();
                    if(_gamePlayerModel.IsMine)
                    {
                        _gamePlayerUI.Activate();
                    }
                })
            .AddTo(_disposable);
    }

    public void Dispose()
    {
        _disposable.Dispose();
    }
}
