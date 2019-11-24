
using protocol.codes;
using server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class UserHandler:IHandler{

        AccountCache account = Caches.account;
        UserCache usercache = Caches.user;

        public void Init() {
            usercache.create("测试账号", account.GetId("admin"));

            usercache.create("测试账号の小号", account.GetId("test"));
            usercache.create("呆萌果甜甜脆", account.GetId("123"));
        }
        public void OnDisconnect(server.clientPeer client) {
            if (usercache.isOnline(client)) {
                usercache.offLine(client);
            }
        }
        public void OnRecive(server.clientPeer client, int subCode, object value) {
            switch (subCode) {
                case UserCode.GET_INFO_REQS: {
                    getInfo(client);
                    break;
                    }
                case UserCode.CREATE_REQS: {
                        UserDto userdto = value as UserDto;
                        create(client, userdto);
                    break;
                    }
                case UserCode.ONLINE_REQS: {
                    online(client);
                    break;
                    }
            }
        }
        public void getInfo(clientPeer client) {
            if (!account.isOnline(client)) {
                //非法登录
                client.Send(OpCode.USER, UserCode.GET_INFO_SRES, -1);
                return;
            }
         
            int accid = account.GetId(client);
            if (!usercache.isExist(accid)) {
                //没有角色
                client.Send(OpCode.USER, UserCode.GET_INFO_SRES, -2);
                return;
            }
            if (usercache.isOnline(client)) {
                //已经在线
                client.Send(OpCode.USER, UserCode.ONLINE_SRES, -3);
                return;
            }
            UserModel model = usercache.getModelByAccid(accid);
            client.Send(OpCode.USER, UserCode.GET_INFO_SRES, new UserDto(model.Id,model.Name, model.Gold, model.Level, model.Exp));

            int uid = usercache.getId(accid);
            usercache.onLine(client, uid);
            client.Send(OpCode.USER, UserCode.ONLINE_SRES, 0);
        }
        public void online(clientPeer client) {
            if (!account.isOnline(client)) {
                //非法登录
                client.Send(OpCode.USER, UserCode.ONLINE_SRES, -1);
                return;
            }
            int accid = account.GetId(client);
            if (!usercache.isExist(accid)) {
                //角色不存在
                client.Send(OpCode.USER, UserCode.ONLINE_SRES, -2);
                return;
            }
            if (usercache.isOnline(client)) {
                //已经在线
                client.Send(OpCode.USER, UserCode.ONLINE_SRES, -3);
                return;
            }
            int uid=usercache.getId(accid);
            usercache.onLine(client, uid);
            client.Send(OpCode.USER, UserCode.ONLINE_SRES, 0);
               
        }
        public void create(clientPeer client,UserDto userMsg) {
            singleExecute.Instance.Execute(()=>{
                        if (!account.isOnline(client)) {
                            //非法登录
                            client.Send(OpCode.USER, UserCode.CREATE_SRES, -1);
                            return;
                        }
                        int accid = account.GetId(client);
                        if (usercache.isExist(accid)) {
                            //角色存在
                            client.Send(OpCode.USER, UserCode.CREATE_SRES, -2);
                            return;
                        }
                        usercache.create( userMsg.Name, accid);
                        client.Send(OpCode.USER, UserCode.CREATE_SRES, 0);
                       
                        //创建成功
                    }
                );
            
        }
        
    }

