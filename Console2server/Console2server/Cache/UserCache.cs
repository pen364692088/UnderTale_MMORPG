using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using server;
using server.Util.Current;


  public  class UserCache {
        ConcurrentDictionary<int, UserModel> idModelDic = new ConcurrentDictionary<int, UserModel>();
        ConcurrentDictionary<int, int> accIdUserIdDic = new ConcurrentDictionary<int, int>();

        public CurrentInt uid = new CurrentInt(0);
        public void create(string name,int accId) {
            UserModel newUser = new UserModel(uid.Add_Get(), name, accId);
            idModelDic.TryAdd(uid.Get(),newUser);
            accIdUserIdDic.TryAdd(accId, uid.Get());
        }
        //判断账号是否有角色
        public bool isExist(int accid){
            return accIdUserIdDic.ContainsKey(accid);
        }
        //根据账号id查找user
        public UserModel getUserByAccId(int accid) {
            int uid = accIdUserIdDic[accid];
            UserModel user = idModelDic[uid];
            return user;

        }
         //根据账号id查找userId
        public int getId(int accid) {
            int uid = accIdUserIdDic[accid];
            return uid;
        }
        private ConcurrentDictionary<clientPeer, int> clientUidDic = new ConcurrentDictionary<clientPeer, int>();
        private ConcurrentDictionary<int, clientPeer> uidClientDic = new ConcurrentDictionary<int, clientPeer>();
        //判断一个连接对象是否在线
        public bool isOnline(clientPeer client){
            return clientUidDic.ContainsKey(client);
        }
        //判断一个id是否在线
        public bool isOnline(int id){
            return uidClientDic.ContainsKey(id);
        }
        public void onLine(clientPeer client,int id){
            clientUidDic.TryAdd(client, id);
            uidClientDic.TryAdd(id, client);
        }
        public void offLine(clientPeer client) {
            int id = clientUidDic[client];
            uidClientDic.TryRemove(id, out client);
            clientUidDic.TryRemove(client, out id);
        }
        //根据连接对象获取user
        public UserModel getModelByClient(clientPeer client) {
            int id = clientUidDic[client];
            return idModelDic[id];
            
        }
        public UserModel getModelByUid(int uid) {
            UserModel model = idModelDic[uid];
            return model;

        }
        public UserModel getModelByAccid( int accid) {
            int id = accIdUserIdDic[accid];
            return idModelDic[id];

        }
        //根据连接对象获取id
        public int getId(clientPeer client) {
            if (!clientUidDic.ContainsKey(client)) {
                throw new ExecutionEngineException("角色不在 在线的字典里");
            }
            return clientUidDic[client];
        }

        //根据id获取连接对象
        public clientPeer getClient(int id) {
            return uidClientDic[id];
        }
    
}
