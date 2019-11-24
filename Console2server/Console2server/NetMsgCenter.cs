using GameServer.Logic;
using protocol.codes;
using server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


   
    public class NetMsgCenter :IAppliaction{

       
        IHandler accountHandle = new AccountHandler();
        IHandler userHandle = new UserHandler();
        IHandler worldHandler = new WorldHandler();
        IHandler syncHandler = new SyncHandler();

        MatchHandler matchHandle = new MatchHandler();
        IHandler chartHandle = new ChartHandler();
    //    FightHandler fightHandler = new FightHandler();
        public void OnDisconnect(clientPeer client) {
        //    fightHandler.OnDisconnect(client);
            worldHandler.OnDisconnect(client);
         //   chartHandle.OnDisconnect(client);
         //   matchHandle.OnDisconnect(client);

            userHandle.OnDisconnect(client);
            accountHandle.OnDisconnect(client);
        }
        public NetMsgCenter() {
         //   matchHandle.startFight += fightHandler.StartFight;
            handlerInit();
        }
        public void handlerInit() {
            accountHandle.Init();
            userHandle.Init();
            worldHandler.Init();
        }
       
        public void OnAccept(clientPeer client) {
           
        }





        public void OnRecive(clientPeer client, server.socketMsg msg) {
            Console.WriteLine("msg.OpCode:" + msg.OpCode);
            switch (msg.OpCode) {
                case OpCode.ACCOUNT: {
                        accountHandle.OnRecive(client, msg.SubCode, msg.value);
                        break;
                    }
                case OpCode.USER: {
                        userHandle.OnRecive(client, msg.SubCode, msg.value);
                        break;
                    }
                case OpCode.MATCH: {
                        matchHandle.OnRecive(client, msg.SubCode, msg.value);
                        break;
                    }
                case OpCode.CHART: {
                        chartHandle.OnRecive(client, msg.SubCode, msg.value);
                        break;
                    }
                //case OpCode.FIGHT: {
                //        fightHandler.OnRecive(client, msg.SubCode, msg.value);
                //        break;
                //    }
                case OpCode.WORLD: {
                    worldHandler.OnRecive(client, msg.SubCode, msg.value);
                    break;
                } case OpCode.SYNC: {
                    syncHandler.OnRecive(client, msg.SubCode, msg.value);
                    break;
                }
                    
                default:
                    break;
            }

        }
    }

