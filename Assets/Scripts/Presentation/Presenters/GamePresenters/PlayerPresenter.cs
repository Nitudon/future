using UnityEngine;
using UniRx;

/// <summary>
/// 同期プレイヤーモデルとその実座標やUIなどのビューのバインダー
/// </summary>
public class PlayerPresenter : MonoBehaviour, System.IDisposable {

    /// <summary>
    /// 同期プレイヤーの実座標や見た目の行動を管理するビュー
    /// </summary>
    [SerializeField]
    private GamePlayerView _gamePlayerView;

    /// <summary>
    /// 同期するデータを管理するプレイヤーモデル
    /// </summary>
    [SerializeField]
    private PlayerModel _gamePlayerModel;

    /// <summary>
    /// プレイヤーのUIを管理するビュー
    /// </summary>
    [SerializeField]
    private GamePlayerUI _gamePlayerUI;

    /// <summary>
    /// 各種バインダーのDisposable
    /// </summary>
    private CompositeDisposable _disposable = new CompositeDisposable();

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {

        // 操作対象のプレイヤーに関してのUI処理
        if (_gamePlayerModel.IsMine)
        {
            _gamePlayerModel.PlayerHp
                .Subscribe(_gamePlayerUI.UpdatePlayerUI)
                .AddTo(_disposable);
        }
        else
        {
            //同期データの位置に基づく座標移動
            _gamePlayerModel.SyncPosition
                .Subscribe(_gamePlayerView.SyncPosition)
                .AddTo(_disposable);
        }
    
        // アクティベート処理
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

    /// <summary>
    /// バインダーの解放
    /// </summary>
    public void Dispose()
    {
        _disposable.Dispose();
    }
}
