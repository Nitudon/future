using Newtonsoft.Json;

namespace AGS.Domains
{

    /// <summary>
    /// 同期データ構造定義
    /// サーバークライアント間のjsonの定義
    /// </summary>

    /// <summary>
    /// 同期データの対象
    /// </summary>
    [System.Serializable]
    public enum SyncType
    {
        // ルーム
        Room = 0,

        // オブジェクト
        Object,

        // プレイヤー
        Player,
    }

    /// <summary>
    /// 送信する実同期データ構造
    /// </summary>
    [System.Serializable]
    [JsonObject("sync_message")]
    public class SyncMessage
    {
        // 同期対象
        [JsonProperty("sync_type")]
        public SyncType SyncType;

        // ペイロード
        [JsonProperty("message")]
        public string Message;
    }

    /// <summary>
    /// ルーム同期データ構造
    /// </summary>
    [System.Serializable]
    [JsonObject("sync_room")]
    public class SyncRoomData
    {
        // 識別ID
        [JsonProperty("room_id")]
        public string Id;

        // ルーム名
        [JsonProperty("name")]
        public string Name;

        // 試合時間
        [JsonProperty("time")]
        public float Time;

        // 参加プレイヤー
        [JsonProperty("players")]
        public SyncPlayerData[] Players;
    }

    /// <summary>
    /// オブジェクト同期データ構造
    /// ワールド内の同期オブジェクトのベースクラス
    /// </summary>
    [System.Serializable]
    [JsonObject("sync_object")]
    public class SyncObjectData
    {
        // 識別ID
        [JsonProperty("id")]
        public string Id;

        // X座標
        [JsonProperty("position_x")]
        public float PositionX;

        // Y座標
        [JsonProperty("position_y")]
        public float PositionY;

        // Z座標
        [JsonProperty("position_z")]
        public float PositionZ;

        // 破壊フラグ
        [JsonProperty("is_destroyed")]
        public bool IsDestroyed;
    }

    /// <summary>
    /// プレイヤー同期データ構造
    /// </summary>
    [System.Serializable]
    [JsonObject("sync_player")]
    public class SyncPlayerData : SyncObjectData
    {
        // プレイヤー識別番号
        [JsonProperty("player_id")]
        public int PlayerId;

        // プレイヤー名
        [JsonProperty("name")]
        public string Name;

        // プレイヤーのHP
        [JsonProperty("hp")]
        public float Hp;
    }

}