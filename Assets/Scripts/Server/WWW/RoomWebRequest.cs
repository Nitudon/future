using System.IO;
using System.Threading.Tasks;
using AGS.Domains;
using UnityEngine;
using UniRx;
using UdonLib.Commons;

namespace AGS.WebRequest
{
    /// <summary>
    /// ルーム周りのWebリクエスト
    /// </summary>
    public class RoomWebRequest
    {
        /// <summary>
        /// リクエストするサーバードメイン
        /// </summary>
        private static readonly string URL_DOMAIN = "localhost:3000";
        
        /// <summary>
        /// ルームデータのフェッチリクエスト
        /// </summary>
        /// <returns>対象となるルームの同期データ</returns>
        public static async Task<SyncRoomData[]> FetchSyncRoomDataAsync()
        {
            return await HttpRequestAsync.GetRequestAsync<SyncRoomData[]>($"http://{URL_DOMAIN}/room/fetchRoom");
        }

        /// <summary>
        /// ルームの入室リクエスト
        /// </summary>
        /// <returns>入室するルームの同期データ</returns>
        public static async Task<SyncRoomData> JoinRoomAsync()
        {
            return await HttpRequestAsync.GetRequestAsync<SyncRoomData>($"http://{URL_DOMAIN}/room/join");
        }
    }
}

