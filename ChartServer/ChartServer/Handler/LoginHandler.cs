using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using protobuff;

namespace ChartServer.Handler
{
    public class LoginHandler : HandlerBase
    {
        public override OperationCode OpCode {
            get { return OperationCode.Login; }
        }


        public override void OnHandlerMsg(OperationRequest request,OperationResponse response, SendParameters parameters, MyClient client)
        {
            userData data= ParameterTools.GetData<userData>(request.Parameters, ParameterCode.UserData, true);
          
            if (ChartServer.Instance.CheckPassword(data.username, data.password)==1)
            {
                client.userdata = ChartServer.Instance.getUserData(data.username);
                client.state = ClientState.Online;
                client.nowRole = client.userdata.roleData[0]; //默认选择第一个
                ParameterTools.SetReturnCode(response.Parameters, ReturnCode.Success);
            }
            else
            {
                ParameterTools.SetReturnCode(response.Parameters, ReturnCode.Fail);
            }
            ParameterTools.AddData(response.Parameters, ParameterCode.UserData, client.nowRole, true);
            // response.Parameters.Add((byte)ParameterCode.UserData, client.ConnectionId);
            // response.Parameters.Add((byte)ParameterCode.UserData, new userData(client.ConnectionId));
        }
    }
}
