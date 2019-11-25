using server;
using server.Util.Current;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


    public class MatchCache
    {

        
        //根据等待id找等待room模型
        public Dictionary<int,MatchRoom>ridRoomDic=new Dictionary<int,MatchRoom>();

        //根据等待id找等待roomid
        public Dictionary<int, int> uidRidDic = new Dictionary<int, int>();


        //重用的房间队列
        public Queue<MatchRoom> roomQue = new Queue<MatchRoom>();

        //房间id

        private CurrentInt id = new CurrentInt(-1);

        public MatchRoom Enter(int uid,clientPeer client){
         
            foreach (MatchRoom mr in ridRoomDic.Values){
                if (mr.isFull())
                {
                    continue;
                }
              
                //没满的话
                mr.Add(uid, client);
                uidRidDic.Add(uid, mr.Id);
                return mr;
            }
         
            //这里说明没有空余房间
            MatchRoom room = null;
            if (roomQue.Count > 0){
                room = roomQue.Dequeue();
            }
            else
            {
                room = new MatchRoom(id.Add_Get());
            }
            room.Add(uid, client);
           
            uidRidDic.Add(uid, room.Id);
            ridRoomDic.Add(room.Id, room);

            Console.WriteLine(uid + "加入房间 :" + room.Id);
            return room;
        }
        public MatchRoom Leave(int uid){
            int rid = uidRidDic[uid];
            MatchRoom room = ridRoomDic[rid];

            Console.WriteLine(uid + "离开房间 :" + room.Id);
            room.Leave(uid);
            uidRidDic.Remove(uid);
            if (room.isEmpty()){
               Destroy(room);
            }
            return room;
        }
        
        public bool isMatching(int uid){

            return uidRidDic.ContainsKey(uid);
        }

        public MatchRoom getRoom(int uid) {
            int rid = uidRidDic[uid];
            MatchRoom room = ridRoomDic[rid];
            return room;
        }
        public void Destroy(MatchRoom room) {
            ridRoomDic.Remove(room.Id);

            foreach (var uid in room.UidClientDic.Keys) {
                uidRidDic.Remove(uid);
            }
            room.Destroy();
            roomQue.Enqueue(room);

            //清除数据
        }
    
}
