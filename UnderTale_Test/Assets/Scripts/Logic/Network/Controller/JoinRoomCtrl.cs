using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExitGames.Client.Photon;
using protobuff;
using UnityEngine.SceneManagement;

public class JoinRoomCtrl : ControllerBase
{
    public override OperationCode OpCode => OperationCode.Room;

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        PhotonEngine.Instance.world = ParameterTools.GetData<WorldData>(operationResponse.Parameters, ParameterCode.WorldData, true);
        PhotonEngine.Instance.isRoomMaster = PhotonEngine.Instance.world.MasterId == PhotonEngine.Instance.myId;

       

        if ((byte)ParameterTools.GetReturnCode(operationResponse.Parameters) == (byte)ReturnCode.Success)
        {
            MyScenceManager.Instance.Load("Room2", CallBack);
        }
    }
    public void CallBack(Scene scene, LoadSceneMode sceneType)
    {
        GameManager.Instance.InitWorld();
    }
}
