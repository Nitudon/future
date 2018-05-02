using WebSocketSharp;
using WebSocketSharp.Net;
using AGS.Websocket;
using UnityEngine;
using UdonLib.Commons;

namespace AGS.Websocket
{
    public class Client
    {
        private WebSocket _connection;
        public WebSocket Connection => _connection;

        public System.Action<object, MessageEventArgs> OnReceiveMessageListener;

        public Client()
        {
            _connection = new WebSocket(ConnectionParameters.WS_CONNECTIONADDRESS);
        }

        public Client(string address)
        {
            _connection = new WebSocket(address);
        }

        public void Connect()
        {
            if (_connection == null)
            {
                InstantLog.StringLogError("Missing websocket");
                return;
            }

            HandleFunc();

            _connection.Connect();
        }

        public void SendMessage(string message)
        {
            if (_connection == null || _connection.IsConnected == false)
            {
                InstantLog.StringLogError("Missing websocket connection");
                return;
            }

            _connection.Send(message);
        }

        public void SendJson(string message)
        {
            SendMessage(JsonUtility.ToJson(message));
        }

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

        private void ReceiveMessageHandle(object sender, MessageEventArgs e)
        {
            OnReceiveMessageListener?.Invoke(sender, e);
        }

    }
}