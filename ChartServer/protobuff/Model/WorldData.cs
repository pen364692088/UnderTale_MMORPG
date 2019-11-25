using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class WorldData
{
    public int id = -1;

    public List<RolerData> userList = new List<RolerData>();
    public List<enemyData> enemyList = new List<enemyData>();

    public List<ItemData> scenceObjList = new List<ItemData>();
    public int MasterId = -1;

    public WorldData()
    {

    }
    public WorldData(int rid, List<RolerData> list, int mid, List<ItemData> itemList)
    {
        id = rid;
        userList = list;
        MasterId = mid;
        scenceObjList = itemList;
    }

    

}
