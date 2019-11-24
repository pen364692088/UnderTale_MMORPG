using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using protobuff;

namespace ChartServer.Handler
{
    public class PlayerHandler : HandlerBase
    {
        public override OperationCode OpCode => OperationCode.Player;

        public override void OnHandlerMsg(OperationRequest request, OperationResponse response, SendParameters parameter, MyClient client)
        {
            switch (ParameterTools.GetData<SubCode>(request.Parameters, ParameterCode.SubCode, false))
            {
                case SubCode.CreatePlayer:
                    {
                        userData data = ParameterTools.GetData<userData>(request.Parameters, ParameterCode.UserData, true);
                        client.room.AddPlayer(client);
                        client.room.MsgBro(EventCode.NewPlayer, request.Parameters, parameter, client);
                        break;
                    }
                case SubCode.SyncPlayerAnimat:
                    {
                        client.room.MsgBro(EventCode.SyncAnimat, request.Parameters, parameter, client);
                        break;
                    }
                case SubCode.SyncPlayerData:
                    {
                        client.room.MsgBro(EventCode.SyncPlayerData, request.Parameters, parameter, client);
                        break;
                    }
                case SubCode.SyncPlayerTranst:
                    {
                        client.room.MsgBro(EventCode.SyncTranst, request.Parameters, parameter, client);
                        break;
                    }
            }


           
        }
    }
}
