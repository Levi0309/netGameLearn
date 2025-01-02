using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ڿͻ��˷��͵�¼���󣬽��û��������뷢�͸�������
/// </summary>
public class LoginRequest : BaseRequest
{
   private LoginPanel loginPanel;
    protected override void Awake()
    {
        requestCode = RequestCode.user;
        actionCode = ActionCode.Login;
        loginPanel = GetComponent<LoginPanel>();
        base.Awake();
    }
 
    public void SendRequest(string username, string password)
    {
        string data = username + "," + password;
        base.SendRequest(data);
    }
    public override void OnResponse(string data)
    {
        ReturnCode returnCode = (ReturnCode)int.Parse(data);
        loginPanel.OnLoginResponse(returnCode);

    }
}
