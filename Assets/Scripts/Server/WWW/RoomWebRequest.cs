using System.IO;
using System.Threading.Tasks;
using AGS.Domains;
using UnityEngine;
using UniRx;

namespace AGS.WebRequest
{
    public class RoomWebRequest
    {
        private static readonly string URL_DOMAIN = "localhost:8080";
        
        public static async Task<RoomData> FetchRoomDataAsync(string id)
        {
            var www = await ObservableWWW.Get($"https://{URL_DOMAIN}/?room_id={id}");
            if(www == null)
            {
                Debug.LogError("www request error");
            }
            return JsonUtility.FromJson<RoomData>(www);
        }

        public static async Task<RoomData> TestRoomDataAsync()
        {
            var www = await ObservableWWW.Get($"http://{URL_DOMAIN}/test");
            if (www == null)
            {
                Debug.LogError("www request error");
            }
            return JsonUtility.FromJson<RoomData>(www);
        }
    }
}

