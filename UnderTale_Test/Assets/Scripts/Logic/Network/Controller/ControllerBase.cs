using ExitGames.Client.Photon;
using protobuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class ControllerBase:MonoBehaviour
{
    public abstract OperationCode OpCode { get; }

    public virtual void Start()
    {
        PhotonEngine.Instance.RegistController(OpCode, this);
    }
    public virtual void OnDestroy()
    {
        PhotonEngine.Instance.RemoveController(OpCode);
    }
    public abstract void OnOperationResponse(OperationResponse operationResponse);
}
