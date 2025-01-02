using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 可以挂载在游戏物体身上 所以继承MonoBehaviour
/// </summary>
public class BaseRequest : MonoBehaviour
{
    protected RequestCode requestCode = RequestCode.None;
    protected ActionCode actionCode = ActionCode.None;
    protected GameFacade facade;
    protected virtual void Awake()
    {
       
        GameFacade.Instance.AddRequest(actionCode, this);
        facade = GameFacade.Instance;
    }
    public virtual void SendRequest() { }
    public virtual void OnResponse(string data) { }
    private void OnDestroy()
    {
        GameFacade.Instance.RemoveRequest(actionCode);
    }
    public virtual void SendRequest(string data)
    {
        facade.SendRequest(requestCode, actionCode, data);
    }

}
