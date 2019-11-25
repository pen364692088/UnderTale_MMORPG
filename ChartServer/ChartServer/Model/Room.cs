using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartServer.Model
{
   public class Room
    {
        public int Id = -1;

        public Dictionary<int, MyClient> IdClientDic = new Dictionary<int, MyClient>();
        public Dictionary<int, enemyData> idEnemyDic = new Dictionary<int, enemyData>();
        public Dictionary<int, ItemData> idItemDic = new Dictionary<int, ItemData>();

        public List<RolerData> userList = new List<RolerData>();
        public List<enemyData> enemyList = new List<enemyData>();

        public List<ItemData> scenceObjList = new List<ItemData>();

        public int MasterId = 0;

        public int maxNum = 4;

        public int objId = 0;

        public ItemData temp = new ItemData();
        public Room()
        {
            Id = -1;
            IdClientDic = new Dictionary<int, MyClient>();
            userList = new List<RolerData>();
            enemyList = new List<enemyData>();
            InitScenceObj();
        }
        public Room(int id)
        {
            Id = id;
            IdClientDic = new Dictionary<int, MyClient>();
            userList = new List<RolerData>();
            enemyList = new List<enemyData>();
            InitScenceObj();
        }
        public bool isEmpty()
        {
            return IdClientDic.Count < maxNum;
        }
        public void SetMaxNum(int num)
        {
            maxNum = num;
        }
        public void SetMaster(int id)
        {
            MasterId = id;
        }
        public void AddPlayer(MyClient client)
        {
            int uid = client.userdata.id;
            if (!IdClientDic.ContainsKey(uid))
            {
                IdClientDic.Add(uid, client);
                userList.Add(client.userdata.roleData[0]);
            }
          
            if (IdClientDic.Count == 1)
            {
                SetMaster(uid);
            }
            //ChartServer.Log("房间人数userList:" + userList.Count.ToString());
            //ChartServer.Log("房间人数IdClientDic:" + IdClientDic.Count.ToString());
        }
        public void RemovePlayer(MyClient client)
        {
            int uid = client.userdata.id;
            if (IdClientDic.ContainsKey(uid))
            {
                IdClientDic.Remove(uid);
                userList.Remove(client.userdata.roleData[0]);
            }
          

            if (IdClientDic.Count == 1)
            {
                SetMaster(userList[0].id);
            }
            //ChartServer.Log("房间人数userList:" + userList.Count.ToString());
            //ChartServer.Log("房间人数IdClientDic:" + IdClientDic.Count.ToString());
        }
        public void AddEnemy(enemyData data)
        {
            int uid = data.id;
            if (!idEnemyDic.ContainsKey(uid))
            {
                idEnemyDic.Add(uid, data);
                enemyList.Add(data);
            }

          
        }
        public void RemoveEnemy(enemyData data)
        {
            int uid = data.id;
            if (idEnemyDic.ContainsKey(uid))
            {
                idEnemyDic.Remove(uid);
                enemyList.Remove(data);
            }
            //ChartServer.Log("房间人数userList:" + userList.Count.ToString());
            //ChartServer.Log("房间人数IdClientDic:" + IdClientDic.Count.ToString());
        }
        public void AddScenceObj(ItemType tpye, int num)
        {
            temp.change(objId++, tpye, num);
            temp.setPos(0, 0f, 0f);
            scenceObjList.Add(temp);
            idItemDic.Add(temp.id, temp);
        }
        Queue<ItemData> itemQueue = new Queue<ItemData>();

        public ItemPackage AddItemPackag(ItemPackage package)
        {
            itemQueue.Clear();
            while (package.itemList.Count > 0)
            {

                itemQueue.Enqueue(package.itemList.Dequeue().setId(objId++));
            }
            return new ItemPackage(itemQueue);
        }
       
        public ItemData RemoveScenceObj(ItemData data)
        {
            if (idItemDic.ContainsKey(data.id))
            {
                idItemDic.Remove(data.id);
            }
            if (scenceObjList.IndexOf(data) != -1)
            {
                scenceObjList.Remove(data);
            }
            return data;


        }
        public void AddScenceObj(ItemType tpye, int num,float x,float y,float z)
        {
           
            ItemData _temp = temp.change(objId++, tpye, num, x, y, z);
            scenceObjList.Add(_temp);
            idItemDic.Add(temp.id, _temp);
        }
        public void InitScenceObj()
        {

           
      
           // scenceObjList.Add(temp.change(objId++, ItemType.Gold, 1, -4.24f, 0.1f, 21.84f));


          
      
            scenceObjList.Add(temp.change(objId++, ItemType.Chest, 1, 34.45918f, -3.40f, 42.10f));

            scenceObjList.Add(temp.change(objId++, ItemType.Chest, 1, 31.19941f, -3.40f, 42.10f));

          //  scenceObjList.Add(temp.change(objId++, ItemType.Knight_woman, 1, -12.73f, 0.15f, 28.78f));
            scenceObjList.Add(temp.change(objId++, ItemType.Fire_woman, 1, 24.49f, -3.72f, 35.43f));

          // scenceObjList.Add(temp.change(objId++, ItemType.Fire_woman, 1, -8.03f, 1f, 41f));
            scenceObjList.Add(temp.change(objId++, ItemType.Knight_woman , 1, -31.65f, 7.07f, -13.87f));


        }
        public WorldData getWorldData()
        {
            return new WorldData(Id, userList, MasterId,scenceObjList);
        }
        public void MsgBro(EventCode code,Dictionary<byte,object> param,SendParameters sendParameters,MyClient client,bool isSelf=false)
        {
            EventData data = new EventData((byte)code, param);
            foreach(var cli in IdClientDic.Values)
            {
                if (cli != client)
                {
                    cli.SendEvent(data, sendParameters);
                }
                else
                {
                    if (isSelf)
                    {
                        cli.SendEvent(data, sendParameters);
                    } 
                }
            }
        }
    }
}
