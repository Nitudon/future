using System.Threading.Tasks;
using AGS.WebRequest;
using UnityEngine;
using Zenject;

/// <summary>
/// ゲームのブート管理を行うクラス
/// </summary>
public class GameRulePresenter : MonoBehaviour 
{

    /// <summary>
    /// 同期するルームモデル
    /// </summary>
    [Inject]
    private RoomModel _roomModel;

    /// <summary>
    /// サーバーとの同期データの送受信を行うプロキシ
    /// </summary>
    [Inject]
    private SyncSubject _syncSubject;

    private void Start()
    {
        SetupRoom("Room1");
    }

    /// <summary>
    /// ゲームのブート処理
    /// </summary>
    /// <param name="roomId">サーバー上のルームの識別ID</param>
    /// <returns></returns>
    public async Task SetupRoom(string roomId)
    {
        // 非同期で依存関係を解決しながら各オブジェクトを初期化
        var room = await RoomWebRequest.JoinRoomAsync();
        _roomModel.Initialize(room);
        await _syncSubject.InitializeAsync();
    }

    public async Task BootInGame()
    {

    }

}
