using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server {
    public interface IAppliaction {

        //断开连接
         void OnDisconnect(clientPeer client);
        //接受数据
         void OnRecive(clientPeer client, socketMsg msg);
        //连接

         void OnAccept(clientPeer client);
    }
}
