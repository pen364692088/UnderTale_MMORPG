using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace protobuff
{
   public enum OperationCode:byte
    {
        Login=1,
        Room,
        Player,
        Monster,
        RegistUser,
        Role,
        Item,
        Talk
    }
}
