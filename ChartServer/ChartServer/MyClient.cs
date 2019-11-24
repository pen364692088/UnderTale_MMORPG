using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChartServer.Handler;
using ChartServer.Model;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;

namespace ChartServer
{
  public  class MyClient : ClientPeer

    {
        public int roomId = -1;
        public ClientState state = ClientState.Offline;
        public Room room;
        public userData userdata;
        public RolerData nowRole;
        public MyClient(InitRequest initRequest) : base(initRequest)
        {
            userdata = new userData();
            ChartServer.Log("客户端上线");
        }
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            if (roomId!=-1)
            {
                Dictionary<byte, object> param = new Dictionary<byte, object>();
                ParameterTools.AddData(param, protobuff.ParameterCode.UserData, userdata, true);
                room.MsgBro(EventCode.ExitPlayer, param, new SendParameters(), this);
                room.RemovePlayer(this);
            }
            ChartServer.Log("客户端下线");
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            //Dictionary<byte, object> dict = new Dictionary<byte, object>();
            //dict.Add(1, "sucess");
            //OperationResponse response = new OperationResponse(1, dict);
            //SendOperationResponse(response, sendParameters);
            ChartServer.Log("客户端请求 opCode:"+ operationRequest.OperationCode);
            HandlerBase handler;
            ChartServer.Instance.OpCodeHandlerDic.TryGetValue(operationRequest.OperationCode, out handler);
            OperationResponse response=new OperationResponse();
            response.OperationCode = operationRequest.OperationCode;
            response.Parameters = new Dictionary<byte, object>();
            if (handler != null)
            {
                handler.OnHandlerMsg(operationRequest,response,sendParameters,this);

                SendOperationResponse(response, sendParameters);
            }
            else
            {
                ChartServer.Log("客户端请求类型不存在 code:"+ operationRequest.OperationCode);
            }
           
        }
    }
}
