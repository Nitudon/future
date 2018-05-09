namespace AGS.Domains{

    [System.Serializable]
    public class SyncRoomData
    {
        public float Time;
    }

    [System.Serializable]
    public class SyncObjectData
    {
        public string Id;
        public float PositionX;
        public float PositionY;
        public float PositionZ;
        public bool IsDestroyed;
    }

    [System.Serializable]
    public class SyncPlayerData : SyncObjectData
    {
        public new int Id;
        public float Hp;
    }

}