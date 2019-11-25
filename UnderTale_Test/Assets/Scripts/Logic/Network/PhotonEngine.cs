

using ExitGames.Client.Photon;
using protobuff;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonEngine : MonoBehaviour,IPhotonPeerListener
{
    public  bool isOnline=true;
    private ConnectionProtocol protocol =ConnectionProtocol.Tcp;
    
    private PhotonPeer peer;
    private string address =null;
    private string serverName = "ChartServer";

    private bool isConnected = false;

    private static PhotonEngine _instance;

    public int myId = -1;

    public WorldData world;

    public bool isRoomMaster = false;
    private bool StartConntect = false;
    public delegate void OnConnectedEvent();
    public event OnConnectedEvent onConnectedEvent;

    public Dictionary<byte, object> parameter=new Dictionary<byte, object>();
    public static PhotonEngine Instance {
        get {
            if (_instance == null)
            {
                _instance = new PhotonEngine();
            }
            return _instance;
        }
    }

    public Dictionary<byte, ControllerBase> OpCodeControlDic = new Dictionary<byte, ControllerBase>();
    private void Awake()
    {

        
        
        _instance = this;

        address = isOnline ? "101.89.165.103:4530" : "127.0.0.1:4530";
        peer = new PhotonPeer(this, protocol);
        onConnectedEvent += successConnectTest;
        //connect();
    }
    void Start()
    {
        
    }

    public void StartConnect()
    {
        StartConntect = true;
    }


    void Update()
    {
        if (StartConntect)
        {
            if (isConnected)
            {
                peer.Service();
            }
            else
            {
                connect();
            }
        }
      
    }
    public void Login(userData data)
    {
        Dictionary<byte, object> userdata = new Dictionary<byte, object>();
        ParameterTools.AddData(userdata, ParameterCode.UserData, data,true);
        peer.OpCustom((byte)OperationCode.Login, userdata, true);
        print("登陆:" + (byte)OperationCode.Login);
    }
    public void JoinRoom()
    {
        peer.OpCustom((byte)OperationCode.Room, new Dictionary<byte, object>(), true);
        print("进入房间" + (byte)OperationCode.Room);
    }
    public void SyncPos(TransData data)
    {
        parameter.Clear();
        ParameterTools.AddData(parameter, ParameterCode.TransData, data, true);
        ParameterTools.AddData(parameter, ParameterCode.SubCode, SubCode.SyncPlayerTranst, false);
        peer.OpCustom((byte)OperationCode.Player, parameter, true);
      
    }
    public void SyncAnim(AnimState data)
    {
        parameter.Clear();
        ParameterTools.AddData(parameter, ParameterCode.AnimatData, data, true);
        ParameterTools.AddData(parameter, ParameterCode.SubCode, SubCode.SyncPlayerAnimat, false);
        peer.OpCustom((byte)OperationCode.Player, parameter, true);

    }
    public void SyncMonsterTrans(TransData data)
    {
        parameter.Clear();
        ParameterTools.AddData(parameter, ParameterCode.TransData, data, true);
        ParameterTools.AddData(parameter, ParameterCode.SubCode, SubCode.SyncMonsterTranst, false);
        peer.OpCustom((byte)OperationCode.Monster, parameter, true);

    }
    public void SyncMonsterAnim(AnimState data)
    {
        parameter.Clear();
        ParameterTools.AddData(parameter, ParameterCode.AnimatData, data, true);
        ParameterTools.AddData(parameter, ParameterCode.SubCode, SubCode.SyncMonsterAnimat, false);
        peer.OpCustom((byte)OperationCode.Monster, parameter, true);
    }
    public void SyncMonsterData(EntityData data)
    {
        parameter.Clear();
        ParameterTools.AddData(parameter, ParameterCode.MonsterData, data, true);
        ParameterTools.AddData(parameter, ParameterCode.SubCode, SubCode.SyncMonsterData, false);
        peer.OpCustom((byte)OperationCode.Monster, parameter, true);

    }
    public void AddEnemy(enemyData data)
    {
        parameter.Clear();
        ParameterTools.AddData(parameter, ParameterCode.MonsterData, data, true);
        ParameterTools.AddData(parameter, ParameterCode.SubCode, SubCode.CreateMonster, false);
        peer.OpCustom((byte)OperationCode.Monster, parameter, true);
    }
    public void SyncPlayerData(EntityData data)
    {
        parameter.Clear();
        ParameterTools.AddData(parameter, ParameterCode.PlayerData, data, true);
        ParameterTools.AddData(parameter, ParameterCode.SubCode, SubCode.SyncPlayerData, false);
        peer.OpCustom((byte)OperationCode.Player, parameter, true);
    }
    public void SyncItemFailing(ItemPackage data)
    {
        parameter.Clear();

        ParameterTools.AddData(parameter, ParameterCode.itemData, data, true);
        ParameterTools.AddData(parameter, ParameterCode.SubCode, SubCode.SyncMonsterLostThings, false);
        peer.OpCustom((byte)OperationCode.Item, parameter, true);
    }
    public void SyncPeopleGetThing(ItemData data)
    {
        parameter.Clear();

        ParameterTools.AddData(parameter, ParameterCode.itemData, data, true);
        ParameterTools.AddData(parameter, ParameterCode.SubCode, SubCode.SyncPeopleGetThing, false);
        
        peer.OpCustom((byte)OperationCode.Item, parameter, true);
       // print("发出捡东西请求结束");
    }
    public void successConnectTest()
    {
        print("连接成功");
        //GameObject.FindGameObjectWithTag("Console").GetComponent<Text>().text = "连接成功.....";
        UIMsgCenter.Instance.Dispatch(AreaCode.UI, UIEvent.Link, 1);
    }
    public void RegistController(OperationCode OpCode,ControllerBase ctrl)
    {
        OpCodeControlDic.Add((byte)OpCode, ctrl);
    }
    public void RemoveController(OperationCode OpCode)
    {
        OpCodeControlDic.Remove((byte)OpCode);
    }
    void connect()
    {
        peer.Connect(address, serverName);
        peer.Service();
    }
    public void DebugReturn(DebugLevel level, string message)
    {

    }

    public void OnEvent(EventData eventData)
    {
        switch (eventData.Code)
        {
            case (byte)EventCode.SyncTranst:
                {
                    EntityManager.Instance.syncTrans(ParameterTools.GetData<TransData>(eventData.Parameters, ParameterCode.TransData, true));
                    break;
                }
            case (byte)EventCode.SyncAnimat:
                {
                    EntityManager.Instance.syncAnim(ParameterTools.GetData<AnimState>(eventData.Parameters, ParameterCode.AnimatData, true));
                    break;
                }
            case (byte)EventCode.NewPlayer:
                {
                    print("新玩家加入");
                    EntityManager.Instance.AddPlayer(ParameterTools.GetData<RolerData>(eventData.Parameters, ParameterCode.UserData, true));
                    break;
                }
            case (byte)EventCode.ExitPlayer:
                {
                    print("离开玩家加入");
                    EntityManager.Instance.RemovePlayer(ParameterTools.GetData<RolerData>(eventData.Parameters, ParameterCode.UserData, true));
                    break;
                }
            case (byte)EventCode.CreateMonster:
                {
                    EntityManager.Instance.AddEnemy(ParameterTools.GetData<enemyData>(eventData.Parameters, ParameterCode.MonsterData, true));
                    break;
                }
            case (byte)EventCode.SyncMonsterAnimat:
                {
                    EntityManager.Instance.syncMonstAnim(ParameterTools.GetData<AnimState>(eventData.Parameters, ParameterCode.AnimatData, true));
                    break;
                }
            case (byte)EventCode.SyncMonsterTranst:
                {
                    EntityManager.Instance.syncMonstTrans(ParameterTools.GetData<TransData>(eventData.Parameters, ParameterCode.TransData, true));
                    break;
                }
            case (byte)EventCode.SyncMonsterData:
                {
                    EntityManager.Instance.syncMonstData(ParameterTools.GetData<EntityData>(eventData.Parameters, ParameterCode.MonsterData, true));
                    break;
                }
            case (byte)EventCode.SyncPlayerData:
                {
                    EntityManager.Instance.syncPlayerData(ParameterTools.GetData<EntityData>(eventData.Parameters, ParameterCode.PlayerData, true));
                    break;
                }
            case (byte)EventCode.SyncMonsterLostThings:
                {
                    ItemManager.Instance.PopItem(ParameterTools.GetData<ItemPackage>(eventData.Parameters, ParameterCode.itemData, true));
                    break;
                }
            case (byte)EventCode.SyncPeopleGetThing:
                {
                    ItemManager.Instance.removeItem(ParameterTools.GetData<ItemData>(eventData.Parameters, ParameterCode.itemData, true));
                    break;
                }



        }
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        ControllerBase ctrl;
        OpCodeControlDic.TryGetValue(operationResponse.OperationCode, out ctrl);
        if (ctrl != null)
        {
            ctrl.OnOperationResponse(operationResponse);
        }
        else if(operationResponse.OperationCode==(byte)OperationCode.Player|| operationResponse.OperationCode == (byte)OperationCode.Monster || operationResponse.OperationCode == (byte)OperationCode.Item)
        {
           
        }
        else
        {
            print("返回结果类型错误 opCode:" + operationResponse.OperationCode);
        }
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        switch (statusCode)
        {
            case StatusCode.Connect:
                {
                    
                    isConnected = true;
                    onConnectedEvent();
                   
                    break;
                }
        }
    }
    public void Execute(int code,object value)
    {

    }
}
