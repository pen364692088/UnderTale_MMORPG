
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using FairyGUI;

public class WelcomePanel : UIBase
{
    
    private GComponent wcmPanel = null;
    private AlertWin alertWin = null;
    
    private Controller pageCtrl = new Controller();
    //触摸开始页
    private GButton wlcBtn = null;
    //登录页
    private GButton loginBtn = null;
    private GTextInput usernameInput = null;
    private GTextInput passwordInput = null;

    //服务器页
    private GButton selectServerBtn = null;
    private GButton backLoginBtn = null;


    //角色页

    private GButton chooseRoleBtn = null;


    //创建页
    void Awake()
    {
       
        //UIConfig.defaultFont = "FZSJ-XZLYJW";
        UIConfig.defaultFont = "Source Han Sans CN Medium";

    }


    void Start()
    {
        Bind(UIEvent.Link,
           UIEvent.Login,
           UIEvent.InitRole
           );
        GRoot.inst.SetContentScaleFactor(1920, 1080);

        wcmPanel = gameObject.GetComponent<UIPanel>().ui;

        UIPackage.AddPackage("GUI/TestPag");
        UIConfig.defaultFont = "FZSJ-XZLYJW";


        alertWin = new AlertWin();

        pageCtrl = wcmPanel.GetController("welcome");


        wlcBtn = wcmPanel.GetChild("wlcBtn").asButton;

        wcmPanel.GetChild("BackGround").asCom.GetTransition("bg").timeScale=0.1f;

        loginBtn = wcmPanel.GetChild("loginBtn").asButton;
        usernameInput = wcmPanel.GetChild("username").asTextInput;
        passwordInput = wcmPanel.GetChild("password").asTextInput;

        selectServerBtn = wcmPanel.GetChild("selectServerBtn").asButton;
        backLoginBtn = wcmPanel.GetChild("backLoginBtn").asButton;

        chooseRoleBtn = wcmPanel.GetChild("chooseRoleBtn").asButton;

        wlcBtn.onClick.Add(linkFn);

        loginBtn.onClick.Add(loginFn);

        selectServerBtn.onClick.Add(selectServerFn);
        backLoginBtn.onClick.Add(TurnBefore);

        chooseRoleBtn.onClick.Add(chooseRoleFn);




        //  alertWin.SetXY(wcmPanel.width / 2-250, wcmPanel.height / 2 -100);
        //  alertWin.Show();
        //  win.contentPane = UIPackage.CreateObject("TestPag", "alertMsgWin").asCom;
        //  win.Show();
    }

    public override void Execute(int EventCode, object value)
    {
        switch (EventCode)
        {
            case UIEvent.Link:
                {
                    if ((int)value == 1)
                    {
                        TurnNext();
                        Debug.Log("跳转登录页");
                    }
                    else
                    {
                        Debug.Log("连接失败");
                    }

                    break;
                }
            case UIEvent.Login:
                {
                    testLogin((ReturnCode)value);
                    break;
                }
            case UIEvent.InitRole:
                {
                  //  UserDto user = value as UserDto;
                   // GameManager.Instance.initUid(user.Id);
                 //   print("UID为:" + GameManager.Instance.GetUid());
                    TurnNext();
                    break;
                }
        }
    }
    
    void TurnBefore()
    {
        if (pageCtrl.selectedIndex > 0)
        {
            pageCtrl.selectedIndex -= 1;
        }

    }

    void TurnNext()
    {

        pageCtrl.selectedIndex += 1;
    }

    void linkFn()
    {
        PhotonEngine.Instance.StartConnect();
    }
    void loginFn()
    {
        alertWin.SetXY(wcmPanel.width / 2 - 250, wcmPanel.height / 2 - 100);

        alertWin.Show();

        PhotonEngine.Instance.Login(new userData(-1, usernameInput.text, passwordInput.text));
        // AccountDto acc = new AccountDto(username, password);
        // socketMsg msg = new socketMsg(OpCode.ACCOUNT, AccountCode.LOGIN, acc);
        //   Invoke("testLogin", 1);
        //  Dispatch(AreaCode.NET, 0, msg);
    }

    public void testLogin(ReturnCode isRight)
    {

        if (isRight == ReturnCode.Success)
        {
            alertWin.changeTitle("登录成功");

            Invoke("alertDel", 1);

            TurnNext();
        }
        else
        {
            alertWin.changeTitle("登录失败,原因:" + isRight);
            Invoke("alertDel", 1);
        }

    }
    public void alertDel()
    {
        alertWin.Hide();
    }


    public void selectServerFn()
    {
        //   socketMsg msg = new socketMsg(OpCode.USER, UserCode.GET_INFO_REQS, null);
        //   Invoke("testLogin", 1);
        //  Dispatch(AreaCode.NET, 0, msg);
        PhotonEngine.Instance.JoinRoom();
    }
    public void selectServerFnBack()
    {

        TurnNext();

    }

    public void chooseRoleFn()
    {
        //socketMsg msg = new socketMsg(OpCode.WORLD, WorldCode.INTO, 3);
        ////   Invoke("testLogin", 1);
        //  Dispatch(AreaCode.NET, 0, msg);
        //   myScenceManager.Instance.Load("Fight");
       
    }


}
