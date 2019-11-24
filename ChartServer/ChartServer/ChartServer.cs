using ChartServer.Handler;
using ChartServer.Model;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net;
using log4net.Config;
using Photon.SocketServer;
using protobuff;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChartServer
{
   public  class ChartServer : ApplicationBase
    {
        private static ChartServer _instance;
        public new static ChartServer Instance {
            get {
                if (_instance == null)
                {
                    _instance = new ChartServer();
                }
                return _instance;
            }
        }
        public ChartServer()
        {
            _instance = this;
        } //单例模式

        private static ILogger log = ExitGames.Logging.LogManager.GetCurrentClassLogger();

        public Dictionary<byte, HandlerBase> OpCodeHandlerDic = new Dictionary<byte, HandlerBase>();

        public List<Room> RoomList = new List<Room>();

        public int roomId = 0;

        public Dictionary<byte, object> paramet = new Dictionary<byte, object>();

        public int uid = 1;
        public Dictionary<string, int> userIdDic = new Dictionary<string, int>();
        public Dictionary<string, string> userPwdDic = new Dictionary<string, string>();
        public Dictionary<int, userData> userDataDic = new Dictionary<int, userData>();

        public void InitAccount()
        {
            userIdDic.Add("test", uid++);
            userIdDic.Add("admin", uid++);
            userIdDic.Add("p123", uid++);

            userPwdDic.Add("test", "test");
            userPwdDic.Add("admin", "admin");
            userPwdDic.Add("p123", "p123");

            userData test = new userData(userIdDic["test"],"test","test");
            userData admin = new userData(userIdDic["admin"], "admin", "admin");
            userData more = new userData(userIdDic["p123"], "p123", "p123");

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Type", RoleType.KnightMale);
            dic.Add("Name", "测试角色");
            dic.Add("Level", 1);
            dic.Add("Exp", 0);
            dic.Add("Health", 100);
            dic.Add("STR", 8);
            dic.Add("DEX", 8);
            dic.Add("LUK", 8);

            dic.Add("PosX", -26.76f);
            dic.Add("PosY", 0.19f);
            dic.Add("PosZ", -13.35f);

            test.RegistRole(dic);
            admin.RegistRole(dic);
            more.RegistRole(dic);

            userDataDic.Add(userIdDic["test"], test);
            userDataDic.Add(userIdDic["admin"], admin);
            userDataDic.Add(userIdDic["p123"], more);
        }

        public int CheckPassword(string name,string pwd) {
            int res = -1;
            if (userPwdDic.ContainsKey(name))
            {
                res = userPwdDic[name] == pwd ? 1 : -1;
            }
            else
            {
                res = -2;
            }
            return res;
        }
        public userData getUserData(string name)
        {
            userData data=null;
            if (userIdDic.ContainsKey(name))
            {
                data = userDataDic[userIdDic[name]];
            }
            return data;
        }
        public Room JoinRoom(MyClient client)
        {
            
            bool findRoom = false;
            Room troom = null;
            foreach (var room in RoomList)
            {
                if (room.isEmpty())
                {
                    troom = room;
                    findRoom = true;
                    break;
                }
            }
            if (!findRoom)
            {
                troom = new Room(roomId++);
                RoomList.Add(troom);
            }
            troom.AddPlayer(client);
            return troom;
        }
       public void RegistHandlers()
        {
            //OpCodeHandlerDic.Add((byte)OperationCode.Login, new LoginHandler());
            Type[] types = Assembly.GetAssembly(typeof(HandlerBase)).GetTypes();
            foreach (var type in types)
            {
                if (type.FullName.EndsWith("Handler"))
                {
                    Activator.CreateInstance(type);
                }
            }
        }

        public static void Log(string str)
        {
            log.Info(str.ToString());
        }
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            return new MyClient(initRequest);
        }
        public void InitServer()
        {
            RegistHandlers();
            InitAccount();
          
            InitLogging();

        }
        protected override void Setup()
        {
           


            InitServer();
          
            Log("Setup ok.");
        }

        protected override void TearDown()
        {
            Log("TearDown ok.");
        }

        protected virtual void InitLogging()

        {

            ExitGames.Logging.LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance); //日志工厂

            GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(this.ApplicationRootPath, "log");  //设置app log位置

            GlobalContext.Properties["LogFileName"] = "UnderTale_" + this.ApplicationName; //设置log文件名

            XmlConfigurator.ConfigureAndWatch(new FileInfo(Path.Combine(this.BinaryPath, "log4net.config")));

        }
        //日志输出

    }
}
