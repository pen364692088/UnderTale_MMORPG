using server;
using System;
using System.Collections.Generic;

public  class SyncHandler:IHandler {
    public WorldRoomCache roomCache = Caches.room;
    public UserCache user = Caches.user;

 
    public void OnRecive(server.clientPeer client, int subCode, object value) {
 
         
        MoveSync(value as UpdateMsg,subCode, client);
          
        
    }

    public void OnDisconnect(server.clientPeer client) {
      
    }
    public void MoveSync(UpdateMsg msg, int subCode, clientPeer client) {
         //  Console.WriteLine(msg.frameCount);
        roomCache.getRoom(user.getId(client)).BroMsg(OpCode.SYNC, subCode, msg, null);
    }
    
    public void Init() {
       
    }
}
