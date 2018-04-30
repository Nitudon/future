using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UdonLib.Commons;

public class GameScenePresenter : SceneManager {

    private static readonly string TITLE_SCENE_NAME = "Title";
    private static readonly string ROOM_SCENE_NAME = "RoomMatching";
    private static readonly string GAME_SCENE_NAME = "MultiPlayersGame";

    public async Task LoadTitleAsync()
    {
        await LoadSceneObservable(TITLE_SCENE_NAME);
    }

    public async Task LoadRoomAsync()
    {

    }

    public async Task LoadGameAsync()
    {

    }
}
