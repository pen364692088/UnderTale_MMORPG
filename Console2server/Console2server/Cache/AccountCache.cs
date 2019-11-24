using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using server.Util.Current;
using server;


    //账号缓存
public class AccountCache {
        //账号对应的模型
        private Dictionary<string, AccountModel> accountDict = new Dictionary<string, AccountModel>();

        private CurrentInt id = new CurrentInt(0);

        //判断存在
        public bool isExist(string acc){
            return accountDict.ContainsKey(acc);
        }
        //注册
        public void Create(string acc,string psw) {
        //    if (!isExist(acc)) {逻辑层写
                AccountModel newAcc = new AccountModel(id.Add_Get(), acc, psw);
                accountDict.Add(newAcc.Account, newAcc);
       //     }
        
        }
        //获取模型
        public AccountModel GetModel(string acc) {

            return accountDict[acc];
        }
        //验证账号密码
        public bool isMatch(string acc, string psw) {
            AccountModel model = accountDict[acc];
            return model.Password == psw;

        }

        private Dictionary<string  ,clientPeer> accClientPeerDic =new Dictionary<string,clientPeer>();
        private Dictionary<clientPeer, string> clientPeerAccDic = new Dictionary<clientPeer, string>();


        public bool isOnline(string acc){
            return accClientPeerDic.ContainsKey(acc);
        }
        public bool isOnline(clientPeer client) {
            return clientPeerAccDic.ContainsKey(client);
        }
        //上线
        public void Online(clientPeer client,string acc) {
            accClientPeerDic.Add(acc, client);
            clientPeerAccDic.Add(client, acc);
        }
        //下线
        public void Offline(clientPeer client) {
            string acc = clientPeerAccDic[client];
            accClientPeerDic.Remove(acc);
            clientPeerAccDic.Remove(client);
        }

        public void Offline(string acc) {
            clientPeer client = accClientPeerDic[acc];
            clientPeerAccDic.Remove(client);
            accClientPeerDic.Remove(acc);
        }

        //获取在线玩家id

        public int GetId(clientPeer client) {
            string acc = clientPeerAccDic[client];
            AccountModel model = accountDict[acc];
            return model.Id;

        }
        public int GetId(string acc) {
           
            AccountModel model = accountDict[acc];
            return model.Id;

        }
    
}
