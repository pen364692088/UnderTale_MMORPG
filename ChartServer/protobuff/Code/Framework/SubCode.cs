using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public enum SubCode:byte
{
    CreateMonster=1,
    SyncMonsterTranst,
    SyncMonsterAnimat,
    SyncMonsterData,
    SyncPlayerData,
    SyncMonsterLostThings,
    SyncPeopleGetThing,
    SyncPlayerTranst,
    SyncPlayerAnimat,
    CreatePlayer,
    ExitPlayer,
}
