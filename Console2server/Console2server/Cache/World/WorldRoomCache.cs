using server;
using System;
using System.Collections.Generic;
public class WorldRoomCache {
    //根据地图id找地图
    public Dictionary<int, WorldRoom> ridRoomDic = new Dictionary<int, WorldRoom>();


    //根据uid找地图id
    public Dictionary<int, int> uidRidDic = new Dictionary<int, int>();


  
  
    //private CurrentInt id = new CurrentInt(-1);
    public void AddMap( params  int[] rid) {
        foreach (var id in rid) {
            WorldRoom map = new WorldRoom(id);
            ridRoomDic.Add(id, map);
        }
       
    }

    public int  Enter(int rid,int uid, clientPeer client) {

        if (!ridRoomDic.ContainsKey(rid)) {
            return -1;
        }
        WorldRoom map = ridRoomDic[rid];

        map.Add(uid,client);

        if (uidRidDic.ContainsKey(uid)) {
            uidRidDic[uid] = rid;
        }
        else {
            uidRidDic.Add(uid, rid);
        }

        Console.WriteLine(uid + "加入地图 :" + map.Id);
        return 0;
    }
    public int Leave(int rid,int uid) {
        if (!ridRoomDic.ContainsKey(rid)) {
            return -1;
        }
        WorldRoom map = ridRoomDic[rid];

        Console.WriteLine(uid + "离开地图 :" + map.Id);
        map.Leave(uid);
      
       
        return 0;
    }

 
    public WorldRoom getRoom(int uid) {
        int rid = uidRidDic[uid];
      
        return ridRoomDic[rid];
    }
    public int getRoomId(int uid) {
     

        return uidRidDic[uid];
    }
    //public void Destroy(WorldRoom room) {
    //    ridRoomDic.Remove(room.Id);

    //    foreach (var uid in room.UidClientDic.Keys) {
    //        uidRidDic.Remove(uid);
    //    }
    //    room.Destroy();
    //    roomQue.Enqueue(room);

    //    //清除数据
    //}
}
