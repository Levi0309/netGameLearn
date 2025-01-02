using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel :BasePanel {

    private Text text;
    private float showTime = 1;
    private string message = null;
    private void Update()
    {
        if (message != null)
        {
            ShowMessage(message);
            message = null;
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        text = GetComponent<Text>();
        text.enabled = false;
        uiMgr.InjectMsgPanel(this);
    }

    public void ShowMessageSync(string msg)
    {
        message = msg;
    }
    public void ShowMessage(string msg)
    {
        text.CrossFadeAlpha(1, 0.2f, false); //ignoreTimeScale = false（受 TimeScale 影响） 使text透明度从1变为0.2f
        text.text = msg;
        text.enabled = true;
        Invoke("Hide", showTime);
       

    }
    private void Hide()
    {
        text.CrossFadeAlpha(0, 1, false);
    }

}
