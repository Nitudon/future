namespace AGS.Domains{

    /// <summary>
    /// ルーム情報、通信単位の情報や同期する対戦でのシステム情報
    /// </summary>
    public struct RoomData{
        public int RoomId;

        public UnityEngine.Vector3 RoomRoot;

        public float TotalGameTime;

        public float CurrentGameTime;
    }

    /// <summary>
    /// ユーザー情報、ゲーム内パラメータやシステム上のユーザーパラメータ
    /// </summary>
    public struct UserData{
        // 属するルーム
        public RoomData Room;
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
    public struct PlayerData{
        public float Hp;

        public UnityEngine.Vector3 Position;
        
    }
}