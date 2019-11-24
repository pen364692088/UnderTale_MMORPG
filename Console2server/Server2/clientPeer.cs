using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace server {
    public class clientPeer {

     //   public EncodeTool encodeTool = null;
        public Socket clientSocket { get; set; }

        public clientPeer() {
             
        //     encodeTool = EncodeTool;
             this.resArgs = new SocketAsyncEventArgs();
             this.resArgs.UserToken = this;
             this.resArgs.SetBuffer(new byte[1024], 0, 1024);
             this.SendArgs = new SocketAsyncEventArgs();
             this.SendArgs.Completed += SendArgs_completed;
         }
        //接受数据

        public delegate void ReciveCompleted(clientPeer client, socketMsg obj);

        public ReciveCompleted reciveCompleted;
        /// 接受的网络套接字
        public SocketAsyncEventArgs resArgs { get; set; }

        /// 接受的缓存区

        private List<byte> dataCache = new List<byte>();

        ///  是否正在处理数据
        private bool isReciveProcess = false;


        public void setSocket(Socket s) {
            clientSocket = s;
        }
   //开始连接
        public void startLink(string ip, int port) {
          //  clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint IP = new IPEndPoint(IPAddress.Parse(ip), port);
            clientSocket.Connect(IP);
            Console.Write("客户端发起请求");
        }
     
    //接受数据
        public void StartRecive(byte[] package) {
            dataCache.AddRange(package);
     //       Console.WriteLine("服务器 package 返回 :" + package.Length);
            if (!isReciveProcess) {
                processRecive();
            }
        }
        public void processRecive() {
            isReciveProcess = true;
       //     Console.WriteLine("processRecive dataCache:" + dataCache.Count);
            byte[] res = EncodeTool.DecodePakeage(ref dataCache);
           
            if (res==null) {
                isReciveProcess = false;
                return;
            }
      //      Console.WriteLine("服务器 res 返回 :" + res.Length);
            socketMsg data = EncodeTool.DecodeMsg(res);
            if (data == null) {
                isReciveProcess = false;
                return;
            }
       //     Console.WriteLine("服务器 data 返回 :" + data.value);
        //    Console.WriteLine(reciveCompleted);
            if (reciveCompleted != null) {
              
                reciveCompleted(this, data);
            }


            processRecive();

            
        }
        //public void recive_Completed() {
        //    processRecive();
        //}
        //断开连接
 
        public void Disconnect() {
            dataCache.Clear();
            isReciveProcess = false;

            sendCache.Clear();
            isSendProcess = false;



            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
            clientSocket = null;
        }
      
    //发送数据

        public delegate void Send_Complete(clientPeer client, string message);
        public Send_Complete sendCompleted;

        public SocketAsyncEventArgs SendArgs;   
        private Queue<byte[]> sendCache=new Queue<byte[]>();
        private bool isSendProcess = false;

        public void Send(int Opcode, int Subcode, object value) {
            socketMsg msg = new socketMsg(Opcode, Subcode, value);
      
            byte[] package = EncodeTool.EncodePackage(EncodeTool.EncodeMsg(msg));

            //  Console.WriteLine("发送数据:"+value);
           // Console.WriteLine(package.Length);
          
            Send(package);
        }
        public void Send(byte[] package){
         sendCache.Enqueue(package);
            if (!isSendProcess) {
                send();
            }
        }
       public void send() {
            isSendProcess = true;
            if (sendCache.Count ==0) {
                isSendProcess = false;
                return;
            }else{
                byte[] package = sendCache.Dequeue();
                SendArgs.SetBuffer(package,0, package.Length);
              //  Console.WriteLine("send...");
                bool res=clientSocket.SendAsync(SendArgs);

               // Console.WriteLine("send... res:" + res);
                if (res==false) {
                    procesSend();
                }
            }
            

        }
        public void SendArgs_completed(object sender, SocketAsyncEventArgs e) {
            procesSend();
        }
        public void procesSend() {
            if (SendArgs.SocketError != SocketError.Success) {
                //发送出错 客户端断开连接
                sendCompleted(this, SendArgs.SocketError.ToString());
            }
            else {
                send();
            }
        }

       
    }
}
