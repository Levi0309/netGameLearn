using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestManager : BaseManager
{
    public RequestManager(GameFacade facade) : base(facade)
    {
        
    }
    private Dictionary<ActionCode,BaseRequest> requestDict = new Dictionary<ActionCode, BaseRequest>();

    /// <summary>
    /// �������
    /// </summary>
    /// <param name="requestCode"></param>
    /// <param name="request"></param>
    public void AddRequest(ActionCode actionCode, BaseRequest request)
    {
        requestDict.Add(actionCode, request);
    }
    public void RemoveRequest(ActionCode actionCode)
    {
        requestDict.Remove(actionCode);
    }
    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="requestCode"></param>
    /// <param name="data"></param>
    public void HandleReponse(ActionCode actionCode, string data)
    {
        BaseRequest request = requestDict.TryGet(actionCode);
        if (request == null)
        {
            Debug.LogWarning("�޷��õ�ActionCode[" + actionCode + "]��Ӧ��Request��");
            return;
        }
        request.OnResponse(data);
    }


}
