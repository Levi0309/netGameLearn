﻿using UnityEngine;
using System.Collections;

public class BasePanel : MonoBehaviour {
    protected UIManager uiMgr;
    private GameFacade gameFacade;
    public GameFacade GameFacade
    {
        get { return gameFacade; }
        set { gameFacade = value; }
    }
    public UIManager UIMgr 
    {
        get { return uiMgr; }
        set { uiMgr = value; } 
    }
    protected void PlayClickSound() 
    {
        gameFacade.PlayNormalSound(AudioManager.Sound_ButtonClick);
    }
    /// <summary>
    /// 界面被显示出来
    /// </summary>
    public virtual void OnEnter()
    {

    }

    /// <summary>
    /// 界面暂停
    /// </summary>
    public virtual void OnPause()
    {

    }

    /// <summary>
    /// 界面继续
    /// </summary>
    public virtual void OnResume()
    {

    }

    /// <summary>
    /// 界面不显示,退出这个界面，界面被关系
    /// </summary>
    public virtual void OnExit()
    {

    }
}
