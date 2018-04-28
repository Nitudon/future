namespace AGS.Domains{

    /// <summary>
    /// ルーム情報、通信単位の情報や同期する対戦でのシステム情報
    /// </summary>
    public struct RoomData{
        public int RoomId;

        public int MasterUserId;

        public UserData[] Users;

        public float TotalGameTime;

        public float CurrentGameTime;
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
    public struct PlayerData{
        public float Hp;

        public UnityEngine.Vector3 Position;
        
    }
}