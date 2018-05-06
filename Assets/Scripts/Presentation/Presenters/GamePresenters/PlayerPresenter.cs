using UnityEngine;
using UniRx;

public class PlayerPresenter : MonoBehaviour, System.IDisposable {

    [SerializeField]
    private GamePlayerView _gamePlayerView;

    [SerializeField]
    private PlayerModel _gamePlayerModel;

    private CompositeDisposable _disposable = new CompositeDisposable();

    public void Initialize()
    {
        _gamePlayerModel.PlayerPosition
            .Subscribe(_gamePlayerView.SyncPosition)
            .AddTo(_disposable);

        _gamePlayerModel.OnActivated
            .Where(active => active)
            .First()
            .Subscribe(_ => _gamePlayerView.ActivatePlayerView())
            .AddTo(_disposable);
    }

    public void Dispose()
    {
        _disposable.Dispose();
    }
}
