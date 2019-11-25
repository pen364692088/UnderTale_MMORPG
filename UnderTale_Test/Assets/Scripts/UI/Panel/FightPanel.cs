using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using FairyGUI;
using System.Collections;

public class FightPanel : UIBase
{
    public static FightPanel Instance = null;
    void Awake()
    {
        Instance = this;
        Bind(UIEvent.ShowDamage,UIEvent.SETHP);
    }



    private GComponent fightCom;
    private GComponent msgCom;


    private MyJoyStickModule joyStick;

    private GButton menuBtn = null;
    private List<GButton> menuBtnList = new List<GButton>();


    private GButton bagBtn = null;


    private GButton attackBtn = null;
    private GButton defenBtn = null;
    private GButton doActionBtn = null;
    

    //   private GProgressBar monsterHPbar = null;
    private GProgressBar PlayerHPbar = null;
    private GProgressBar PlayerCPbar = null;

    private GComponent damageCom = null;
    private GTextField numberText = null;
    private Transition damageT = null;


    private Window menuwindow = null;

    private bool isCanMove = false;
    private Vector3 v3 = new Vector3();



    public RolerData playerData = null;

    public Vector2 data = new Vector2();
    //private float speed = 0.3f;

    //private Timer t = null;

    //private Vector3 oldPos = new Vector3();

    //private Vector3 vec =  Vector3.zero;

    //private Vector3 lastPos = new Vector3();
    public GComponent getCom()
    {
        return fightCom;
    }

    public void Update()
    {

    }
    void Start()
    {




        //  UIConfig.modalLayerColor = new Color(0f, 0f, 0f, 0.4f);


        fightCom = gameObject.GetComponent<UIPanel>().ui;




        menuBtn = fightCom.GetChild("menuBtn").asButton;
        bagBtn = fightCom.GetChild("BagBtn").asButton;

        attackBtn = fightCom.GetChild("attackBtn").asButton;
        defenBtn = fightCom.GetChild("SkillOneBtn").asButton;

        doActionBtn = fightCom.GetChild("doActionBtn").asButton;

        //   monsterHPbar = fightCom.GetChild("monstorHP").asProgress;
        PlayerHPbar = fightCom.GetChild("Health").asProgress;
        PlayerCPbar = fightCom.GetChild("Capability").asProgress;

        damageCom= UIPackage.CreateObject("TestPag", "damageNum").asCom;
        numberText = damageCom.GetChild("attackNum").asTextField;
        damageT = damageCom.GetTransition("t0");

        menuwindow = new Window();

        menuwindow.contentPane = fightCom.GetChild("menu").asCom;
        GComponent fra = menuwindow.contentPane.GetChild("frame").asCom;
        menuBtnList.Add(fra.GetChild("role").asButton);
        menuBtnList.Add(fra.GetChild("bag").asButton);
        menuBtnList.Add(fra.GetChild("skill").asButton);
        menuBtnList.Add(fra.GetChild("task").asButton);
        menuBtnList.Add(fra.GetChild("exitMap").asButton);
        menuBtnList.Add(fra.GetChild("exitGame").asButton);

        //  menuwindow.modal = true;
        menuwindow.Hide();

        msgCom = UIPackage.CreateObject("TestPag", "MsgCom").asCom;
        fightCom.AddChild(msgCom);
        msgCom.GetChild("skill").asCom.GetChild("closeButton").asButton.onClick.Add(roleClickFn);
        msgCom.visible = false;

        joyStick = new MyJoyStickModule(fightCom);
        joyStick.onStart.Add(joyStickOnStart);
        joyStick.onMove.Add(joyStickOnMove);
        joyStick.onEnd.Add(joyStickOnEnd);

        menuBtn.onClick.Add(menuFn);
        menuBtnList[0].onClick.Add(roleClickFn);
        menuBtnList[1].onClick.Add(roleClickFn);
        menuBtnList[2].onClick.Add(roleClickFn);

        //  bagBtn.onClick.Add(InitGame);



        attackBtn.onClick.Add(attackFn);
        defenBtn.onTouchBegin.Add(defenFn);
        defenBtn.onTouchEnd.Add(defenEndFn);

        doActionBtn.onClick.Add(doActionFn);

        PinchGesture pinch = new PinchGesture(fightCom);
        pinch.onAction.Add(changeCamera);



        fightCom.AddChild(damageCom);

        StartCoroutine("sendInput");

        setPlayerHP(GameManager.Instance.SelfPlayer.SelfData);
    }

     IEnumerator sendInput()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            //Debug.Log(data);
            if (isCanMove)
            {

                GameManager.Instance.input.SetHV(data.x.ToString("0.00"), data.y.ToString("0.00"));
            }
            else
            {

            }
        }
    }
    public override void Execute(int EventCode, object value)
    {
        switch (EventCode)
        {
            case UIEvent.SETHP:
                {
                    //   print(value);
                    setPlayerHP(value as EntityData);

                    break;
                }

            case UIEvent.ShowDamage:
                {
                    showDamage(value as V3Text);

                    break;
                }

        }
    }
    public void joyStickOnStart()
    {
        isCanMove = true;
        data = new Vector2();
    }
    public void joyStickOnMove(EventContext context)
    {

        
        data = (Vector2)context.data;
        if (Mathf.Abs( data.x) <=0.2)
        {
            data.x = 0;
        }
        if (data.x >= 0.8)
        {
            data.x = 1;
        }
        if (data.x <= -0.8)
        {
            data.x = -1;
        }
        if (Mathf.Abs(data.y) <= 0.2)
        {
            data.y = 0;
        }
        if (data.y >= 0.8)
        {
            data.y = 1;
        }
        if (data.y <= -0.8)
        {
            data.y = -1;
        }
       // Debug.Log(data);
       
        
       

    }
    // 设置血量
    public void setMonsterHpPos(Vector3 v3)
    {
        v3 = GRoot.inst.WorldToLocal(v3);
    }

    public void setPlayerHP(EntityData data)
    {
        ////playerData = data;
        //Debug.Log(data);
        //Debug.Log(data.type);
        //Debug.Log(data.id);
        //Debug.Log(PlayerHPbar);
        PlayerHPbar.value = data.health;
        PlayerHPbar.max = data.nowHealth;
        
        //PlayerCPbar.max = playerData.capability;
        //PlayerCPbar.value = playerData.nowCapability;


    }
    public void showDamage(V3Text num)
    {
        
        numberText.text = num.text;
       Vector3 v3 = GRoot.inst.WorldToLocal(num.pos);
        damageCom.SetXY(v3.x, v3.y);
        damageT.Play();
    }
    //设置掉落物
    //public void failObjFn(itemFailObj obj)
    //{
    //    GComponent item = UIPackage.CreateObject("TestPag", "failItem").asCom;
    //    switch (obj.item.type)
    //    {
    //        case ItemCode.Money:
    //            {
    //                item.GetChild("icon").icon = ItemURL.Gold;
    //                break;
    //            }
    //    }

    //    fightCom.AddChild(item);
    //    v3 = GRoot.inst.WorldToLocal(obj.pos);
    //    item.SetXY(v3.x, v3.y);

    //    ItemFailList.Add(obj.pos, item);
    //    item.GetTransition("scroll").Play();
    //    item.onClick.Add(() => {
    //        ActorManager.Instance.AddItem(obj.item);
    //        item.GetTransition("failObj").Play(() => {
    //            ItemFailList.Remove(obj.pos);
    //            fightCom.RemoveChild(item);

    //        });

    //    });
    //    // print(fightCom._transitions.Count);
    //}

    public void joyStickOnEnd(EventContext context)
    {
        Debug.Log("触摸结束");
        isCanMove = false;
        GameManager.Instance.input.SetHV("0", "0");
        //if (MsgCenter.Instance.isFight)
        //{
        //    MoveManager.Instance.stopInput();
        //}

    }
    public void menuFn()
    {

        menuwindow.Show();

    }
    //public void InitGame()
    //{
    //    print("开始帧同步");
    //    Dispatch(AreaCode.GAME, GameEvent.INTOWORLD, null);
    //}

    public void attackFn()
    {
        //  print("攻击");
        GameManager.Instance.input.AddKey(AnimationCode.SkillOne);
    }
    public void doActionFn()
    {
        GameManager.Instance.input.AddKey(AnimationCode.SkillTwo);
    }
    public void defenFn()
    {
        //   print("攻击");
        GameManager.Instance.input.AddKey(AnimationCode.SkillOne);
    }
    public void defenEndFn()
    {
        //   print("攻击");
        GameManager.Instance.input.AddKey(AnimationCode.SkillOne);
        //   MoveManager.Instance.action(SyncCode.DEFENCE_DOWN);
    }
    public void changeCamera(EventContext context)
    {

    }

    public void roleClickFn()
    {
        menuwindow.Hide();
        playerData = GameManager.Instance.getPlayerData();
        upDateMsgCom();
        msgCom.visible = !msgCom.visible;

    }
    public void upDateMsgCom()
    {
        upDateRole();
        upDateBag();
    }
    public void upDateRole()
    {
        GComponent role = msgCom.GetChild("role").asCom;
        role.GetChild("STRnum").asTextField.text = playerData.STR.ToString();
        role.GetChild("DEXnum").asTextField.text = playerData.DEX.ToString();

        role.GetChild("level").asTextField.SetVar("level", playerData.level.ToString()).FlushVars();
        role.GetChild("health").asTextField.SetVar("health", playerData.health.ToString()).SetVar("nowHealth", playerData.nowHealth.ToString()).FlushVars();

        role.GetChild("attackNum").asTextField.SetVar("minDamage", (playerData.STR + 1).ToString()).SetVar("maxDamage", (10 + playerData.STR * 2).ToString()).FlushVars();

        //   role.GetChild("defenNum").asTextField.SetVar("defence", playerData.defence.ToString());
    }
    public void upDateBag()
    {
        GComponent bag = msgCom.GetChild("bag").asCom;
        GList list = bag.GetChild("bag").asList;

        for (int i = 0; i < playerData.itemBag.Count; i++)
        {
            playerData.itemBag[i].type = ItemType.Gold;
            string url = ItemMsg.getURL(playerData.itemBag[i].type);

            list.GetChildAt(i).asButton.icon = url;
        }


    }


}
