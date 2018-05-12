using System.IO;
using System.Threading.Tasks;
using AGS.Domains;
using UnityEngine;
using UniRx;
using UdonLib.Commons;

namespace AGS.WebRequest
{
    public class RoomWebRequest
    {
        private static readonly string URL_DOMAIN = "localhost:8080";
        
        public static async Task<SyncRoomData> FetchSYncRoomDataAsync(string id)
        {
            return await HttpRequestAsync.GetRequestAsync<SyncRoomData>($"http://{URL_DOMAIN}/?room_id={id}");
        }

        public static async Task<SyncRoomData> TestSYncRoomDataAsync()
        {
            return await HttpRequestAsync.GetRequestAsync<SyncRoomData>($"http://{URL_DOMAIN}/test");
        }

        public static async Task<SyncRoomData> JoinRoomAsync()
        {
            return await HttpRequestAsync.GetRequestAsync<SyncRoomData>($"http://{URL_DOMAIN}/room/join");
        }

    }
}

