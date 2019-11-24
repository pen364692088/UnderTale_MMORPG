using server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public interface IHandler {
             void OnRecive(clientPeer client, int subCode, object value);
            void OnDisconnect(clientPeer client);
            void Init();
    }

 