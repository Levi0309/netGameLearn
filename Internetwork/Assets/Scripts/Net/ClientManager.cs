using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using Common;
/// <summary>
/// �������������˵�socket����
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
    /// ��������������� GameFacade��InitManager��������
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

            Debug.LogWarning("�޷����ӵ��������ˣ������������磡��" + e);
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
            int count = clientsocket.EndReceive(ar);//���յ��˼�������
            msg.ReadMessage(count, (actionCode, data) =>
            {
                //��������
                facade.HandleResponse(actionCode, data);
                //�����ݴ��ݸ�requestManager
                Debug.Log("���յ�������");
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
        Debug.Log("��������"+ "requestCode " +requestCode+ "actionCode " + actionCode +"data:"+ data);
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
            Debug.LogWarning("�޷��رո��������˵����ӣ���" + e);
        }
    }
}
