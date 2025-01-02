using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using Common;
/// <summary>
/// 用来管理跟服务端的socket连接
/// </summary>
public class ClientManager : BaseManager
{
    private const string IP = "127.0.0.1";
    private const int PORT = 6688;
    private Socket clientsocket;
    private Message msg = new Message();

    public ClientManager(GameFacade facade) : base(facade)
    {
    }
    // <summary>
    /// 与服务器建立连接 GameFacade的InitManager方法调用
    /// </summary>
    public override void OnInit()
    {
        base.OnInit();
        clientsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


        try
        {
            Start();
            clientsocket.Connect(IP, PORT);
        }
        catch (Exception e)
        {

            Debug.LogWarning("无法连接到服务器端，请检查您的网络！！" + e);
        }

    }
    private void Start()
    {
        clientsocket.BeginReceive(msg.Data,msg.StartIndex,msg.RemainIndex, SocketFlags.None,ReceiveCallBack,null);
    }
    
    private void ReceiveCallBack(IAsyncResult ar)
    {
      
        try
        {
            int count = clientsocket.EndReceive(ar);//接收到了几个数据
            msg.ReadMessage(count, (actionCode, data) =>
            {
                //处理数据
                facade.HandleResponse(actionCode, data);
                //把数据传递给requestManager
                Debug.Log("接收到了数据");
                //requestManager.HandleResponse(requestCode, actionCode, data);
            });
            Start();
        }
        catch (Exception e)
        {

            Debug.Log(e);
        }
    }

    
    public void SendRequest(RequestCode requestCode,ActionCode actionCode,string data)
    {
        Debug.Log("发送数据"+ "requestCode " +requestCode+ "actionCode " + actionCode +"data:"+ data);
        byte[] buffer= Message.PackData(requestCode, actionCode, data);
        clientsocket.Send(buffer);
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        try
        {
            clientsocket.Close();
        }
        catch (Exception e)
        {
            Debug.LogWarning("无法关闭跟服务器端的连接！！" + e);
        }
    }
}
