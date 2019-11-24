using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Logic {
    public class FightHandler:IHandler{

        public void OnRecive(server.clientPeer client, int subCode, object value) {
            
        }

        public void OnDisconnect(server.clientPeer client) {
            
        }

        public void StartFight(List<int> uidList) {

        }


        public void Init() {
            throw new NotImplementedException();
        }
    }
}
