using Photon.SocketServer;
using protobuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartServer.Handler
{
    class MosnterHandler : HandlerBase
    {
        public override OperationCode OpCode => OperationCode.Monster;

        public override void OnHandlerMsg(OperationRequest request, OperationResponse response, SendParameters parameter, MyClient client)
        {
            switch(ParameterTools.GetData<SubCode>(request.Parameters, ParameterCode.SubCode, false))
            {
                case SubCode.CreateMonster:
                    {
                       enemyData data= ParameterTools.GetData<enemyData>(request.Parameters, ParameterCode.MonsterData, true);
                        client.room.AddEnemy(data);
                        client.room.MsgBro(EventCode.CreateMonster, request.Parameters, parameter, client);
                        break;
                    }
                case SubCode.SyncMonsterAnimat:
                    {
                        client.room.MsgBro(EventCode.SyncMonsterAnimat, request.Parameters, parameter, client);
                        break;
                    }
                case SubCode.SyncMonsterData:
                    {
                        client.room.MsgBro(EventCode.SyncMonsterData, request.Parameters, parameter, client);
                        break;
                    }
                case SubCode.SyncMonsterTranst:
                    {
                        client.room.MsgBro(EventCode.SyncMonsterTranst, request.Parameters, parameter, client);
                        break;
                    }
            }
          
        }
    }
}
