using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using protobuff;

namespace ChartServer.Handler
{
    public class TalkHandler : HandlerBase
    {
        public override OperationCode OpCode => OperationCode.Talk;

        public override void OnHandlerMsg(OperationRequest request, OperationResponse response, SendParameters parameter, MyClient client)
        {
         
              client.room.MsgBro(EventCode.TalkMsg, request.Parameters, parameter, client,true);
        }
    }
}
