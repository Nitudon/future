using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UniRx;
using UdonLib.Commons;

public class ScenePresenter : SceneManager {

    private static readonly string TITLE_SCENE_NAME = "Title";
    private static readonly string ROOM_SCENE_NAME = "RoomMatching";
    private static readonly string GAME_SCENE_NAME = "MultiPlayersGame";

    private float _progress = 0.0f;
    public float Progress => _progress;

    public async Task LoadTitleAsync()
    {
        _progress = await LoadSceneObservable(TITLE_SCENE_NAME);
    }

    public async Task LoadRoomAsync()
    {
        _progress = await LoadSceneObservable(ROOM_SCENE_NAME);
    }

    public async Task LoadGameAsync()
    {
        _progress = await LoadSceneObservable(GAME_SCENE_NAME);
    }
}
