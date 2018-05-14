using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Net;
using AGS.Websocket;
using UnityEngine;
using UdonLib.Commons;
using UniRx;

namespace AGS.Websocket
{
    /// <summary>
    /// Websocketクライアントクラス
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Websocketコネクション
        /// </summary>
        private WebSocket _connection;
        public WebSocket Connection => _connection;

        /// <summary>
        /// メッセージのハンドリングコールバック
        /// </summary>
        public System.Action<object, MessageEventArgs> OnReceiveMessageListener;

        /// <summary>
        /// コンストラクタ、疎通
        /// </summary>
        public Client()
        {
            _connection = new WebSocket(ConnectionParameters.WS_CONNECTIONADDRESS);
        }

        /// <summary>
        /// コンストラクタ、アドレスを指定して疎通
        /// </summary>
        /// <param name="address"></param>
        public Client(string address)
        {
            _connection = new WebSocket(address);
        }

        /// <summary>
        /// Websocketのコネクト処理
        /// </summary>
        /// <returns></returns>
        public async Task ConnectAsync()
        {

            if (_connection == null)
            {
                InstantLog.StringLogError("Missing websocket");
                return;
            }

            HandleFunc();

            _connection.Connect();
            await new WaitUntil(() => _connection.IsAlive);
        }

        /// <summary>
        /// Websocketのディスコネクト処理
        /// </summary>
        /// <returns></returns>
        public async Task DisconnectAsync()
        {

            if (_connection == null)
            {
                InstantLog.StringLogError("Missing websocket");
                return;
            }

            HandleFunc();

            _connection.Close();
            await new WaitUntil(() => !_connection.IsAlive);
        }

        /// <summary>
        /// Websocketでのメッセージの送信
        /// </summary>
        /// <param name="message">送信するメッセージ</param>
        public void SendMessage(string message)
        {
            if (_connection == null || _connection.IsConnected == false)
            {
                InstantLog.StringLogError("Missing websocket connection");
                return;
            }

            _connection.Send(message);
        }

        /// <summary>
        /// Jsonパースを噛ませて送信
        /// </summary>
        /// <param name="message"></param>
        public void SendJson(string message)
        {
            SendMessage(JsonUtility.ToJson(message));
        }

        /// <summary>
        /// イベントハンドリング
        /// </summary>
        private void HandleFunc()
        {
            _connection.OnOpen += (sender, e) =>
            {
                InstantLog.StringLog("Websocket Open", StringExtensions.TextColor.cyan);
            };

            _connection.OnError += (sender, e) =>
            {
                InstantLog.StringLog("Websocket Error", StringExtensions.TextColor.red);
            };

            _connection.OnClose += (sender, e) =>
            {
                InstantLog.StringLog("Websocket Close", StringExtensions.TextColor.magenta);
            };

            _connection.OnMessage += ReceiveMessageHandle;
        }

        /// <summary>
        /// メッセージの受信ハンドリング
        /// </summary>
        /// <param name="sender">送信者</param>
        /// <param name="e">受信メッセージ</param>
        private void ReceiveMessageHandle(object sender, MessageEventArgs e)
        {
            OnReceiveMessageListener?.Invoke(sender, e);
        }

    }
}