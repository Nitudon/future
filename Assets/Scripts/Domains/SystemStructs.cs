namespace AGS.Domains{

    /// <summary>
    /// ルーム情報、通信単位の情報や同期する対戦でのシステム情報
    /// </summary>
    [System.Serializable]
    public struct RoomData{
        public string Id;

        public string Name;

        public PlayerData[] Players;
    }

    /// <summary>
    /// ユーザー情報、ゲーム内パラメータやシステム上のユーザーパラメータ
    /// </summary>
    public struct UserData{
        // ユーザーの識別ID
        public int UserId;
        // ユーザーの表示名
        public string UserName;
        // プレイヤー情報
        public PlayerData PlayerParam;   
    }

    /// <summary>
    /// プレイヤー情報、ゲーム中のプレイヤーに関するデータ
    /// </summary>
    [System.Serializable]
    public struct PlayerData{
        public int Id;

        public string Name;

        public float Hp;

        public float x;

        public float y;

        public float z;
    }
}