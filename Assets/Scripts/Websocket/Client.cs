using WebSocketSharp;
using WebSocketSharp.Net;
using AGS.Websocket;
using UnityEngine;
using UdonLib.Commons;

public class Client{

    private WebSocket _connection;
    public WebSocket Connection => _connection;

    public Client(){
        _connection = new WebSocket(ConnectionParameters.WS_CONNECTIONADDRESS);
    }

    public Client(string address){
        _connection = new WebSocket(address);
    }

    public void Connect(){
        if(_connection == null){
            InstantLog.StringLogError("Missing websocket");
            return;
        }

        _connection.OnOpen += (sender, e) =>
        {
            InstantLog.StringLog("Websocket Open", StringExtensions.TextColor.cyan);
        };

        _connection.OnMessage += (sender, e) =>
        {
            InstantLog.StringLog("WebSocket Send Message : " +  e.Data, StringExtensions.TextColor.green);
        };

        _connection.OnError += (sender, e) =>
        {
            InstantLog.StringLog("Websocket Error", StringExtensions.TextColor.red);
        };

        _connection.OnClose += (sender, e) =>
        {
            InstantLog.StringLog($"Websocket E", StringExtensions.TextColor.magenta);
        };

        _connection.Connect();
    }

    public void SendMessage(string message)
    {
        if (_connection == null || _connection.IsConnected == false)
        {
            InstantLog.StringLogError("Missing websocket connection");
            return;
        }

        message = JsonUtility.ToJson(message);

        _connection.Send(message);
    }

    public void SendJson(string message){
        SendMessage(JsonUtility.ToJson(message));
    }

}
