using server;
using System;
using System.Collections.Generic;


  public  class WorldRoom {
         public int Id;
        public Dictionary<int,clientPeer> UidClientDic{get;private set;}
        public List<int> UidList { get; private set; }
        public int MaxNum=25;

        //public List<int> getAllUid() {
        //    return UidClientDic.Keys.ToList();
        //}
        public WorldRoom(int Id) {
            this.Id=Id;
            this.UidClientDic = new Dictionary<int, clientPeer>();
            this.UidList = new List<int>();
        }

      
        public bool isFull(){
             return UidClientDic.Count>=MaxNum;
        }
        public bool isEmpty()
        {
            return UidClientDic.Count==0;
        }
        public int mapPlayerNum(){
            return UidList.Count;
        }
         public void  Add(int Uid,clientPeer client){
             UidClientDic.Add(Uid, client);
             UidList.Add(Uid);
        }
         public void Leave(int Uid){
            UidClientDic.Remove(Uid);
            UidList.Remove(Uid);
        }
        
      
        public void BroMsg(int opcode, int subcode, object value,clientPeer thisclient) {

            socketMsg msg = new socketMsg(opcode, subcode, value);

            byte[] package = EncodeTool.EncodePackage(EncodeTool.EncodeMsg(msg));

            foreach (var client in UidClientDic.Values) {
                if (client != thisclient) {
                    client.Send(package);
                }
            }
        }
}
