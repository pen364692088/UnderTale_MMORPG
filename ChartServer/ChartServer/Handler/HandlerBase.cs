using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
namespace ChartServer.Handler
{
   public abstract class HandlerBase
    {
        public abstract protobuff.OperationCode OpCode { get; }

        public abstract void OnHandlerMsg(OperationRequest request, OperationResponse response,SendParameters parameter, MyClient client);

        public HandlerBase()
        {
            ChartServer.Instance.OpCodeHandlerDic.Add((byte)OpCode, this);
        }
    }
}
