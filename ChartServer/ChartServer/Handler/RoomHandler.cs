using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using protobuff;

namespace ChartServer.Handler
{
    public class RoomHandler : HandlerBase
    {
        public override OperationCode OpCode => OperationCode.Room;

         public Dictionary<byte, object> param = new Dictionary<byte, object>();
        public override void OnHandlerMsg(OperationRequest request, OperationResponse response, SendParameters parameter, MyClient client)
        {
            if (client.state == ClientState.Online)
            {
                client.room = ChartServer.Instance.JoinRoom(client);
                client.roomId = client.room.Id;

                ParameterTools.SetReturnCode(response.Parameters, ReturnCode.Success);

                ParameterTools.AddData(response.Parameters, ParameterCode.WorldData, client.room.getWorldData(), true);
                // response.Parameters.Add((byte)ParameterCode.WorldData, client.room.IdClientDic.Count);
                param.Clear();
                ParameterTools.AddData(param, ParameterCode.UserData, client.nowRole, true);
                client.room.MsgBro(EventCode.NewPlayer, param, parameter, client);
            }
        
        }
    }
}
