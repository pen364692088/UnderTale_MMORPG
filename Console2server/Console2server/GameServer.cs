using server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer {
    class Program {
        static void Main(string[] args) {
            serverPeer server = new serverPeer();
            server.SetIAppliaction(new NetMsgCenter());
            server.Start(2333,20);

            Console.ReadKey();
        }
    }
}
