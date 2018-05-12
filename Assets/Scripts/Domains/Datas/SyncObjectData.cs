using Newtonsoft.Json;

namespace AGS.Domains{

    public enum SyncType
    {
        Room = 0,
        Object,
        Player,
    }

    [System.Serializable]
    [JsonObject("sync_room")]
    public class SyncRoomData
    {
        [JsonProperty("id")]
        public string Id;
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("time")]
        public float Time;
        [JsonProperty("players")]
        public SyncPlayerData[] Players;
    }

    [System.Serializable]
    [JsonObject("sync_message")]
    public class SyncMessage
    {
        [JsonProperty("sync_type")]
        public SyncType SyncType;
        [JsonProperty("message")]
        public string Message;
    }

    [System.Serializable]
    [JsonObject("sync_object")]
    public class SyncObjectData
    {
        [JsonProperty("id")]
        public string Id;
        [JsonProperty("position_x")]
        public float PositionX;
        [JsonProperty("position_y")]
        public float PositionY;
        [JsonProperty("position_z")]
        public float PositionZ;
        [JsonProperty("is_destroyed")]
        public bool IsDestroyed;
    }

    [System.Serializable]
    [JsonObject("sync_player")]
    public class SyncPlayerData : SyncObjectData
    {
        [JsonProperty("id")]
        public new int Id;
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("hp")]
        public float Hp;
    }

}