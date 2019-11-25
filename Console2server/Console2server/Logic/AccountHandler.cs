using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using protocol.codes;

using server;

//账号处理逻辑层
namespace GameServer.Logic {
    public class AccountHandler :IHandler{
        AccountCache account = Caches.account;


        public void Init() {
            account.Create("admin", "admin");
            account.Create("test", "test");
            account.Create("123", "123");
        }
        public void OnRecive(server.clientPeer client, int subCode, object value) {
            switch (subCode) {
                case AccountCode.REGIST_CREQ:{
                        AccountDto dto = value as AccountDto;
               /*         Console.WriteLine(dto.Account);
                        Console.WriteLine(dto.Password); */
                        regist(client, dto.Account, dto.Password);
                        break;
                    }
                case AccountCode.LOGIN: {
                        AccountDto dto = value as AccountDto;
                        login(client, dto.Account, dto.Password);
                  /*      Console.WriteLine(dto.Account);
                        Console.WriteLine(dto.Password);*/
                        break;
                    }
                default: {
                       break;
                    }
            
            }
        }
        public void regist(clientPeer client, string acc, string psw) {

            singleExecute.Instance.Execute(() => {
                if (account.isExist(acc)) {

                    //账号存在
                    client.Send(OpCode.ACCOUNT, AccountCode.REGIST_SRES, -1);
                    return;
                }
                if (string.IsNullOrEmpty(acc)) {
                    //账号不合法
                    client.Send(OpCode.ACCOUNT, AccountCode.REGIST_SRES, -2);
                    return;
                }
                if (string.IsNullOrEmpty(psw) || psw.Length < 4 || psw.Length > 16) {
                    //密码不合法
                    client.Send(OpCode.ACCOUNT, AccountCode.REGIST_SRES, -3);
                    return;
                }
                account.Create(acc, psw);
                client.Send(OpCode.ACCOUNT, AccountCode.REGIST_SRES,0);
            });
        }
        public void login(clientPeer client, string acc, string psw) {
            singleExecute.Instance.Execute(() => {
                if (!account.isExist(acc)) {
                    //账号不存在
                    client.Send(OpCode.ACCOUNT, AccountCode.LOGIN, -1);
                    return;
                }
                if (account.isOnline(acc)){
                    //账号在线
                    client.Send(OpCode.ACCOUNT, AccountCode.LOGIN, -2);
                    return;
                }
                if (!account.isMatch(acc, psw)) {
                    //密码不匹配
                    client.Send(OpCode.ACCOUNT, AccountCode.LOGIN, -3);
                }
                else {
                    account.Online(client, acc);
                    client.Send(OpCode.ACCOUNT, AccountCode.LOGIN, 0);
                  
                }
            });
        }
        //下线
        public void OnDisconnect(server.clientPeer client) {
            
                if (account.isOnline(client)) {
                    account.Offline(client);
                }
          
        }


    }
}
