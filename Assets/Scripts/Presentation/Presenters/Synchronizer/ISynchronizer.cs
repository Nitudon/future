public interface ISynchronizer
{
    void SendSyncData(string json);
    void ReceiveSyncData(string json);
}