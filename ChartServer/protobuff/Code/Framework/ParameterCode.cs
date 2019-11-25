using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace protobuff
{
    public enum ParameterCode:byte
    {
        SubCode= 1,
        ReturnCode,
        RegistData,
        UserData,
        itemData,
        MonsterData,
        PlayerData,
        WorldData,
        TransData,
        AnimatData,
        DamagData,
        TalkData
    }
}
