namespace AGS.Domains{

    /// <summary>
    /// ルーム情報、通信単位の情報や同期する対戦でのシステム情報
    /// </summary>
    public struct Room{
        public int RoomId;

        public UnityEngine.Vector3 RoomRoot;

        public float TotalGameTime;

        public float CurrentGameTime;
    }

    /// <summary>
    /// ユーザー情報、ゲーム内パラメータやシステム上のユーザーパラメータ
    /// </summary>
    public struct User{
        // 属するルーム
        public Room Room;
        // ユーザーの識別ID
        public int UserId;
        // ユーザーの表示名
        public string UserName;
        // プレイヤー情報
        public Player PlayerParam;
       
    }

    /// <summary>
    /// プレイヤー情報、ゲーム中のプレイヤーに関するデータ
    /// </summary>
    public struct Player{
        public float Hp;

        public UnityEngine.Vector3 Position;
        
    }
}