using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace server {
    //客户端连接池
    public  class clientPeerPool {
        private Queue<clientPeer> pool;
        public clientPeerPool(int num) {
            pool = new Queue<clientPeer>(num);
        }
        public void Enqueue(clientPeer c) {
            pool.Enqueue(c);
        }
        public clientPeer Dequeue() {
            return pool.Dequeue();
        }
    }
}
