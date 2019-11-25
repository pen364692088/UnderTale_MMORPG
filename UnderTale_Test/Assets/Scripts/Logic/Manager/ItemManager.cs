using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemManager: ManagerBase
{
    private static ItemManager _instance;

    public static ItemManager Instance {
        get {
            if (_instance == null)
            {
                _instance = new ItemManager();
            }
            return _instance;
        }
    }
    [HideInInspector]
    public  Dictionary<int, ItemData> idItemOnTheGroundDic = new Dictionary<int, ItemData>();
    public  Dictionary<int, GameObject> idItemObjOnTheGroundDic = new Dictionary<int, GameObject>();
    public  Dictionary<GameObject, int> ObjIdDic = new Dictionary<GameObject,int >();
    [HideInInspector]
    public   Queue<ItemData> nowNeedPopItem = new Queue<ItemData>();
    ItemData temp;
    private float flash_time = 0.3f;
    private float timer = 0;
    public void Awake()
    {
        _instance = this;
    }
    public void init()
    {
        foreach(var i in PhotonEngine.Instance.world.scenceObjList)
        {
            nowNeedPopItem.Enqueue(i);
           
        }
    }
    public void PopItem(ItemPackage package)
    {
       
        while (package.itemList.Count > 0)
        {
            //ItemData temp = package.itemList.Dequeue();
            //nowNeedPopItem.Enqueue(temp);
            nowNeedPopItem.Enqueue(package.itemList.Dequeue());
            //print(" 新来了 nowNeedPopItem Id" + temp.id);

        }
        
    }

    public override void seflUpdate()
    {
       // print("当前爆出物品数量" + nowNeedPopItem.Count);
        if (nowNeedPopItem.Count>0)
        {
            //print("弹出物品1");
            timer += Time.deltaTime;
            if(timer> flash_time)
            {
              
                CheckPop();
                timer = 0;
            }
           
        }
       
    }
    public void CheckPop()
    {
         temp = nowNeedPopItem.Dequeue();
        GameObject obj=null;
        switch (temp.type)
        {
            case ItemType.Gold:
                {
                   obj =Resources.Load<GameObject>("Items/item_One") as GameObject;
                    break;
                }
            case ItemType.Chest:
                {
                    obj = Resources.Load<GameObject>("Items/Chest") as GameObject;
                    break;
                }
            case ItemType.Fire_woman:
                {
                    obj = Resources.Load<GameObject>("Items/NPC_FireWoman") as GameObject;
                    break;
                }
            case ItemType.Knight_woman:
                {
                    obj = Resources.Load<GameObject>("Items/NPC_KnightF") as GameObject;
                    break;
                }
            case ItemType.Food:
                {
                    obj = Resources.Load<GameObject>("Items/item_One") as GameObject;
                    break;
                }
        }

        //print("弹出物品2");
        obj= GameObject.Instantiate(obj, new Vector3(temp.x, temp.y, temp.z), Quaternion.identity);

        idItemOnTheGroundDic.Add(temp.id, temp);
        idItemObjOnTheGroundDic.Add(temp.id, obj);
        ObjIdDic.Add(obj, temp.id);
    }
   public void PeopleGetThing(GameObject obj)
    {
       // print("发出捡东西请求开始");
        int id;
        ObjIdDic.TryGetValue(obj, out id);
        if (id>-1)
        {
            PhotonEngine.Instance.SyncPeopleGetThing(idItemOnTheGroundDic[id]);
        }
    }
    public void removeItem(ItemData item)
    {
       // print("接受捡东西响应");
        GameObject obj;
        idItemObjOnTheGroundDic.TryGetValue(item.id, out obj);
        if (obj != null)
        {
            idItemObjOnTheGroundDic.Remove(item.id);
            idItemOnTheGroundDic.Remove(item.id);
            ObjIdDic.Remove(obj);
            obj.GetComponent<ScenceTrigger>().beTaked();
        }
    }

}
