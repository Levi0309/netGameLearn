using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFacade : MonoBehaviour
{
    private static GameFacade _instance;
    public static GameFacade Instance { get { return _instance; } } 

    private void Awake()
    {
        if (_instance != null) 
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;   
    }
    private CameraManager cameraMgr;
    private PlayerManager playerManager;
    private UIManager uiMgr;
    private RequestManager requestMgr;
    private ClientManager clientMgr;
    private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        InitManager();
        Debug.Log("GameFacade Start");
    }

    private void InitManager()
    {
        uiMgr = new UIManager(this);
        cameraMgr = new CameraManager(this);
        playerManager = new PlayerManager(this);
        requestMgr = new RequestManager(this);
        clientMgr = new ClientManager(this);
        audioManager = new AudioManager(this);
        

        uiMgr.OnInit();
        cameraMgr.OnInit();
        playerManager.OnInit();
        requestMgr.OnInit();
        clientMgr.OnInit();
        audioManager.OnInit();
    }
    /// <summary>
    /// 添加和移除请求
    /// </summary>
    /// <param name="requestCode"></param>
    /// <param name="request"></param>
    public void AddRequest(ActionCode actionCode, BaseRequest request)
    {
        requestMgr.AddRequest(actionCode, request);
    }
    public void RemoveRequest(ActionCode actionCode)
    {
        requestMgr.RemoveRequest(actionCode);
    }
    public void HandleResponse(ActionCode actionCode, string data)
    {
        requestMgr.HandleReponse(actionCode, data);
    }
    public void PlayBgSound(string soundName) 
    {
        audioManager.PlayBgSound(soundName);
    }
    public void PlayNormalSound(string soundName)
    {
        audioManager.PlayNormalSound(soundName);
    }
    private void DestroyManager() 
    {
        uiMgr.OnDestroy();
        cameraMgr.OnDestroy();
        playerManager.OnDestroy();
        requestMgr.OnDestroy();
        clientMgr.OnDestroy();
        audioManager.OnDestroy();
    }
    private void OnDestroy()
    {
        DestroyManager();
    }
    public void ShowMessage(string msg) 
    {
        uiMgr.ShowMessage(msg);
    }
    /// <summary>
    /// 通过gameFacade中介调用clientMgr里的发送请求
    /// </summary>
    /// <param name="requestCode"></param>
    /// <param name="actionCode"></param>
    /// <param name="data"></param>
    public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
    {

        clientMgr.SendRequest(requestCode, actionCode, data);
    }
   
}
