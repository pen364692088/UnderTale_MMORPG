using server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


   public class MatchRoom {

        public int Id;
        public Dictionary<int,clientPeer> UidClientDic{get;private set;}
        public List<UserModel> ReadyUidList { get; private set; }
        public int MaxNum=3;

        public List<int> getAllUid() {
            return UidClientDic.Keys.ToList();
        }
        public MatchRoom(int Id){
            this.Id=Id;
            this.UidClientDic = new Dictionary<int, clientPeer>();
            
        }

      
        public bool isFull(){
             return UidClientDic.Count>=MaxNum;
        }
        public bool isEmpty()
        {
            return UidClientDic.Count==0;
        }
        public int readyNum(){
            return ReadyUidList.Count;
        }
         public void  Add(int Uid,clientPeer client){
             UidClientDic.Add(Uid, client);
        }
         public void Leave(int Uid){
            UidClientDic.Remove(Uid);
        }
        

         public bool isAllReady() {
             return ReadyUidList.Count >= MaxNum;
         }
        public void Destroy() {
            UidClientDic.Clear();
            ReadyUidList.Clear();
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
