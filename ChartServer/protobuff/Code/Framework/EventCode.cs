using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum EventCode:byte
{
    NewPlayer=1,
    SyncTranst,
    SyncAnimat,
    ExitPlayer,
    SyncDamange,
    SyncMonsterTranst,
    SyncMonsterAnimat,
    SyncMonsterData,
    CreateMonster,
    SyncPlayer,
    SyncMonsterLostThings,
    SyncPeopleGetThing,
    SyncPlayerData,
    TalkMsg
}
