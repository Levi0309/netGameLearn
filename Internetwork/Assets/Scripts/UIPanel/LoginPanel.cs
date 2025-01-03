﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Common;
using System;
public class LoginPanel : BasePanel {

    private Button closeButton;
    private InputField usernameIF;
    private InputField passwordIF;
    private LoginRequest loginRequest;
    //private Button loginButton;
    //private Button registerButton;
    private void Start()
    {
        loginRequest=GetComponent<LoginRequest>();
        usernameIF = transform.Find("UsernameLabel/UsernameInput").GetComponent<InputField>();
        passwordIF = transform.Find("PasswordLabel/PasswordInput").GetComponent<InputField>();
        closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(OnCloseClick);
        transform.Find("LoginButton").GetComponent<Button>().onClick.AddListener(OnLoginClick);
        transform.Find("RegisterButton").GetComponent<Button>().onClick.AddListener(OnRegisterClick);
    }
    /// <summary>
    /// 注册
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void OnRegisterClick()
    {
        PlayClickSound();
        UIMgr.PushPanel(UIPanelType.Register);
    }
    /// <summary>
    /// 登录
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void OnLoginClick()
    {
        PlayClickSound();
        string msg="";
        if (string.IsNullOrEmpty(usernameIF.text))
        {
            msg += "用户名不能为空";
        }
        else if (string.IsNullOrEmpty(passwordIF.text))
        {
            msg += "密码不能为空";
        }
        if (msg != "")
        {
            UIMgr.ShowMessage(msg);
            return;
        }
        loginRequest.SendRequest(usernameIF.text, passwordIF.text);
    }
    public void OnLoginResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {
          
        }
        else
        {
            
            UIMgr.ShowMessageAsync("用户名或密码错误,请重新输入！");
        }
    }
    private void OnCloseClick()
    {
        PlayClickSound();
        UIMgr.PopPanel();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        EnterAnimation();

    }
    public override void OnExit()
    {
        base.OnExit();
        HideAnimation();
    }
    private void EnterAnimation()
    {
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.2f);
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(Vector3.zero, 0.2f);
    }
    private void HideAnimation()
    {
        transform.DOScale(0, 0.3f);
        transform.DOLocalMoveX(1000, 0.3f).OnComplete(() => gameObject.SetActive(false));
    }
}
