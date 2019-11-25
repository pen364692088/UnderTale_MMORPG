
using protocol.codes;
using server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public delegate void StartFight(List<int>uidList);
    public class MatchHandler:IHandler{

        public StartFight startFight;

        private MatchCache match = Caches.match;

        private UserCache user = Caches.user;
        public void OnRecive(server.clientPeer client, int subCode, object value) {
            switch (subCode) {
                    case MatchCode.ENTER_REQS:{
                        Enter(client);
                        break;
                    }
                    case MatchCode.LEAVE_REQS: {
                        Leave(client);
                         break;
                     }
                    case MatchCode.READY_REQS: {
                        Ready(client);
                         break;
                    }
                    default: {

                        break;
                    }
                
                }
            }

        public void Enter(clientPeer client) {
            singleExecute.Instance.Execute(() => {
                int uid = user.getId(client);

                if (!isLegal(client,1)) {
                    //若不合法
                    return;
                }
                MatchRoom room = match.Enter(uid,client);

                //广播给其他玩家 有玩家进入

                UserModel model = Caches.user.getModelByUid(uid);
                UserDto dto = new UserDto(model.Id, model.Name, model.Gold, model.Level, model.Exp);

                room.BroMsg(OpCode.MATCH, MatchCode.ENTTER_BRO, dto, client);

                MatchRoomDto roomDto = makeRoomDto(room);
                client.Send(OpCode.MATCH, MatchCode.ENTER_SRES, roomDto);
            });
        }
        public MatchRoomDto makeRoomDto(MatchRoom room) {
            MatchRoomDto roomdto= new MatchRoomDto();
            foreach (var uid in room.UidClientDic.Keys) {
                UserModel model = Caches.user.getModelByUid(uid);
                UserDto dto = new UserDto(model.Id, model.Name, model.Gold, model.Level, model.Exp);
                roomdto.uidUserDict.Add(uid, dto);
                roomdto.uidList.Add(uid);

            }
         
            return roomdto;
        }
        public void Leave(clientPeer client) {

            singleExecute.Instance.Execute(() => {

                if (!isLegal(client,2)) {
                    //若是非法登录
                    return;
                }
                int uid = user.getId(client);
                MatchRoom room = match.getRoom(uid);

                UserModel model = Caches.user.getModelByUid(uid);
                UserDto dto = new UserDto(model.Id, model.Name, model.Gold, model.Level, model.Exp);


                match.Leave(uid);

                client.Send(OpCode.MATCH, MatchCode.LEAVE_SRES, 0);
                //广播其他人有人离开了



                room.BroMsg(OpCode.MATCH, MatchCode.LEAVE_BRO, dto, client);


            });
        }
        public bool isLegal(clientPeer client,int type) {

            int uid = user.getId(client);

            switch (type) {
                case 1: { //加入检查

                        if (!user.isOnline(uid)) {
                            //不在线
                            return false;
                        }
                        if (match.isMatching(uid)) {
                            //若在等待队列,重复加入
                            return false;
                        }
                    break;
                    }
                case 2: {
                        //离开检查
                        if (!user.isOnline(uid)) {
                            //不在线
                            return false;
                        }
                        if (!match.isMatching(uid)) {
                            //若不在等待队列
                            return false;
                        }
                        break;
                    }
                case 3: {
                        //断开检查
                        if (!user.isOnline(uid)) {
                            //不在线
                            return false;
                        } 
                        if (!match.isMatching(uid)) {
                            //重复加入
                            return false;
                        }
                        break;
                    }

            }
         
        

            return true;
          
        }
        public void Ready(clientPeer client) {
            singleExecute.Instance.Execute(() => {
                if (!isLegal(client, 3)) {
                    return;
                }
                int uid = user.getId(client);
                MatchRoom room = match.getRoom(uid);
              


                room.BroMsg(OpCode.MATCH, MatchCode.READY_BRO, uid, null);

                if (room.isAllReady()) {
                    //如果全部玩家都准备好了
                    //广播进入
                    startFight(room.getAllUid());
                    room.BroMsg(OpCode.MATCH, MatchCode.START_BRO, 0 ,null);
                  
                   // match.Destroy(room);
                }
                
              
               
            });
        }
        public void OnDisconnect(server.clientPeer client) {

            if(user.isOnline(client)){
                int uid = user.getId(client);
                if (match.isMatching(uid)) {
                    if (!isLegal(client, 3)) {
                        //不合法
                        return;
                    }

                    Leave(client);
                }
              
              
                 
           
            }
           
           
        }


        public void Init() {
            throw new NotImplementedException();
        }
    }

