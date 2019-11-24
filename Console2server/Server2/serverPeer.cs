using server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace server {
    public class serverPeer {

        // socket端口
        static Socket server = null;

        // 连接数 限制
        static Semaphore semaphore=null;

        //客户端 连接池
        static clientPeerPool clientPool = null;

        //应用层
        private IAppliaction appliaction;

        public void SetIAppliaction(IAppliaction app) {
            this.appliaction = app;
        }
       
        public void Start(int port, int num) {
            try {
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
             
                semaphore = new Semaphore(num,num);
                clientPool = new clientPeerPool(num);
                clientPeer tempClientPeer = null;

                for (int i = 0; i < num; i++) {
                    tempClientPeer = new clientPeer();

                    tempClientPeer.resArgs.Completed += recevie_complet;
                    tempClientPeer.reciveCompleted = reciveCompleted;
                    tempClientPeer.sendCompleted = DisConnect;
                    clientPool.Enqueue(tempClientPeer);
                }

                server.Bind(new IPEndPoint(IPAddress.Any, port));
                server.Listen(10);

                Console.WriteLine("服务器开启成功 队列长度:"+num);

                startAccept(null);
            }
            catch (Exception e) {
                Console.WriteLine("服务器开启失败");
                Console.WriteLine(e.Message);

            }

        }

        //连接数据

        //开始连接
        private void startAccept(SocketAsyncEventArgs e) {
            if (e == null) {
                e = new SocketAsyncEventArgs();
                e.Completed += accept_complet;
            }
            bool flag = server.AcceptAsync(e);
       //     Console.WriteLine("server.AcceptAsync flag:" + flag);
            if (flag==false) {
                processAccept(e);
            }

        }



        private void accept_complet(object sender, SocketAsyncEventArgs e) {
            processAccept(e);
        }
       
       //处理连接
        private void processAccept(SocketAsyncEventArgs e) {

            
         
            semaphore.WaitOne();
           // Socket client = e.AcceptSocket;
            clientPeer client = clientPool.Dequeue();
            client.clientSocket=e.AcceptSocket;
         
            Console.WriteLine("客户端连接  ip:" + client.clientSocket.RemoteEndPoint.ToString());
            //应用层用客户端数据
          //  appliaction.OnAccept(client);
            startRecive(client);
            e.AcceptSocket = null;
            startAccept(e);
        }
        
        //接受
       
        //public void test(string str) {
        //    byte[] res = Encoding.Default.GetBytes(str.Length.ToString() + str);

        //}

        //开始接受数据
        private void startRecive(clientPeer client) {
            try {
                bool flag = client.clientSocket.ReceiveAsync(client.resArgs);
          //      Console.WriteLine(client.clientSocket.RemoteEndPoint.ToString()+" 客户端 startRecive flag:"+flag);
                if (flag==false) {
                    processRecive(client.resArgs);
                }
            }
            catch (Exception e) {

                Console.WriteLine(e.Message);
                throw;
            }
        }
        private void processRecive(SocketAsyncEventArgs e) {
            clientPeer client = e.UserToken as clientPeer;
            SocketAsyncEventArgs res=client.resArgs;
             int length=res.BytesTransferred;
             if (res.SocketError == SocketError.Success && length > 0) {
                 byte[] data = new byte[length];
                 Buffer.BlockCopy(res.Buffer, 0, data, 0, length);
                 //dataCache.Insert(data);
                 //byte[] Decode = encodeTool.DecodePakeage(ref dataCache);
           //      Console.WriteLine("server.processRecive :" + data.Length);
                 client.StartRecive(data); //客户端自身处理数据
                 //尾递归
                 startRecive(client);
             }
             else { //断开连接
                 if (length==0) {
                     if (res.SocketError == SocketError.Success) {
                         //客户端主动断开连接
                         DisConnect(client, "客户端主动断开连接");
                     }
                     else {
                         //客户端 网络异常断开
                         DisConnect(client, "客户端网络异常断开 :" + client.resArgs.SocketError.ToString());
                     }
                 }
             }
        }
        //接受队列排到的函数
        private void recevie_complet(object sender, SocketAsyncEventArgs e) {
            processRecive(e);
        }
        //一条数据解析完成之后的回调
        private void reciveCompleted(clientPeer client,socketMsg msg) {
            //给应用层
            appliaction.OnRecive(client, msg);

        }
        //发送数据

        //断开连接
        public void DisConnect(clientPeer client, string res) {
            try {
                if (client == null) {
                    throw new Exception("指定client为空");
                }
                Console.WriteLine("客户端 ip:" + client.clientSocket.RemoteEndPoint.ToString() + " 已断开连接 原因:" + res);
                //通知应用层 
                appliaction.OnDisconnect(client);

                
                client.Disconnect();
                clientPool.Enqueue(client);
                semaphore.Release();
                

            }
            catch (Exception e) {

                Console.WriteLine(e.Message);
                throw;
            }

           
        }
    }
}
