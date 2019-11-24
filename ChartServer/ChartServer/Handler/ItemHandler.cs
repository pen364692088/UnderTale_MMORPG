using Photon.SocketServer;
using protobuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartServer.Handler
{
    class ItemHandler : HandlerBase
    {
        public override OperationCode OpCode => OperationCode.Item;

        public ItemData temp;
        public override void OnHandlerMsg(OperationRequest request, OperationResponse response, SendParameters parameter, MyClient client)
        {
            switch (ParameterTools.GetData<SubCode>(request.Parameters, ParameterCode.SubCode, false))
            {
                case SubCode.SyncPeopleGetThing:
                    {
                        temp = ParameterTools.GetData<ItemData>(request.Parameters, ParameterCode.itemData, true);
                        switch (temp.type)
                        {
                            case ItemType.Chest:
                                {
                                    
                                    break;
                                }
                            case ItemType.Gold:
                                {
                                    client.nowRole.AddBagItem(client.room.RemoveScenceObj(temp));
                                 
                                    break;
                                }
                        }
                        client.room.MsgBro(EventCode.SyncPeopleGetThing, request.Parameters, parameter, client, true);
                        break;
                    }
                case SubCode.SyncMonsterLostThings:
                    {
                     
                        Dictionary<byte, object> tempParameter = new Dictionary<byte, object>();
                        ParameterTools.AddData(tempParameter, ParameterCode.itemData, client.room.AddItemPackag(ParameterTools.GetData<ItemPackage>(request.Parameters, ParameterCode.itemData, true)), true);
                        ParameterTools.AddData(tempParameter, ParameterCode.SubCode, SubCode.SyncMonsterLostThings, false);

                        client.room.MsgBro(EventCode.SyncMonsterLostThings, tempParameter, parameter, client,true);
                        break;
                    }
            }
                    
        }
    }
}
