using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;
using server;

namespace Client2 {
    class Program {
      
        static void Main(string[] args) {
            clientPeer client = new clientPeer();
          
            client.startLink("127.0.0.1", 23333);
            while (true) {

            }
        }
    }
}
