using server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 public   class WorldHandler :IHandler{
     public WorldRoomCache room = Caches.room;
     public UserCache user = Caches.user;


     public void OnRecive(server.clientPeer client, int subCode, object value) {
         switch (subCode) {
             case WorldCode.INTO: {
                 int rid = (int)value;
                 MapInto(rid, client);
                 break;
                 }
             case WorldCode.EXIT: {
                 int rid = (int)value;
                 MapLeave(client);
                 MapInto(rid, client);
                     break;
                 }
         }
     }

     public void OnDisconnect(server.clientPeer client) {
         int uid=user.getId(client);
         room.Leave(room.getRoomId(uid),uid);
     }
     public void MapLeave(clientPeer client) {
         int uid = user.getId(client);
         room.Leave(room.getRoomId(uid), uid);
     }
     public void MapInto(int rid,clientPeer client) {
         room.Enter(rid, user.getId(client), client);

         client.Send(OpCode.WORLD, WorldCode.INTO, room.getRoom(user.getId(client)).UidList);
     }
     public void Init() {
         room.AddMap(1,2,3);
     }
 }
