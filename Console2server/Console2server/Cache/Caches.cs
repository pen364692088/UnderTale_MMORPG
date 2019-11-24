using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public  class Caches {

        public static AccountCache account { get; set; }
        public static UserCache user { get; set; }
        public static MatchCache match { get; set; }
        public static WorldRoomCache room { get; set; }
        static Caches() {
            account=new AccountCache();
            user = new UserCache();
            match = new MatchCache();
            room = new WorldRoomCache();
        }
    
}
