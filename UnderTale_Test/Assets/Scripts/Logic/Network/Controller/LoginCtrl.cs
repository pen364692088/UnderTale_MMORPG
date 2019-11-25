using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExitGames.Client.Photon;
using protobuff;
using UnityEngine;
using UnityEngine.UI;

public class LoginCtrl : ControllerBase
{
    public override OperationCode OpCode=>OperationCode.Login;


    public override void OnOperationResponse(OperationResponse operationResponse)
    {
       userData data =ParameterTools.GetData<userData>( operationResponse.Parameters,ParameterCode.UserData,true);
        print("id:"+data.id.ToString());
        PhotonEngine.Instance.myId = data.id;

         

       
        UIMsgCenter.Instance.Dispatch(AreaCode.UI, UIEvent.Login, ParameterTools.GetReturnCode(operationResponse.Parameters));
        
    }
}
